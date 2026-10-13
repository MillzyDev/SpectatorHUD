/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

using System.Linq.Expressions;

namespace SpectatorHUD.Expressions
{
    /// <summary>
    /// Represents a mathematical linear expression of form <c>ax+b</c>. In which <c>a</c> and <c>b</c> are constants.
    /// Constructs a compiled lamba expression which may be evaluated with respect to <c>x</c>. 
    /// </summary>
    /// <typeparam name="T">Numeric type in which the expression operated on.</typeparam>
    public class LinearExpression<T> where T : struct
    {
        private delegate T LinearFunction(T x);

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

        public T Evaluate(T x)
        {
            return this._function.Invoke(x);
        }
    }
}