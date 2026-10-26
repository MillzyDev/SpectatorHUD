/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace SpectatorHUD.Expressions;

/// <summary>
///     Represents a mathematical linear expression of form <c>ax+b</c>. In which <c>a</c> and <c>b</c> are constants.
///     Constructs a compiled lamba expression which may be evaluated with respect to <c>x</c>.
/// </summary>
/// <typeparam name="T">Numeric type in which the expression operated on.</typeparam>
public class LinearExpression<T> where T : struct
{
    private readonly LinearFunction _function;

    public LinearExpression(T aValue, T bValue)
    {
        ConstantExpression a = Expression.Constant(aValue, typeof(T));
        ConstantExpression b = Expression.Constant(bValue, typeof(T));
        ParameterExpression x = Expression.Parameter(typeof(T), "x");
        BinaryExpression ax = Expression.Multiply(a, x); // sighhh non binary expression when #theythem
        BinaryExpression axPlusB = Expression.Add(ax, b);
        Expression<LinearFunction> lambdaX = Expression.Lambda<LinearFunction>(axPlusB, x);
        this._function = lambdaX.Compile();
    }

    [Pure]
    public T Evaluate(T x)
    {
        return this._function.Invoke(x);
    }

    private delegate T LinearFunction(T x);
}