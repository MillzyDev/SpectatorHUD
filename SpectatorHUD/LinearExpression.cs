using System.Linq.Expressions;

namespace SpectatorHUD
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
            BinaryExpression ax = Expression.Multiply(a, x);
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