using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AiBi.Test.Common
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            Expression right = new ExParameterVisitor(expr2.Parameters[0], expr1.Parameters[0]).Visit(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, right), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            Expression right = new ExParameterVisitor(expr2.Parameters[0], expr1.Parameters[0]).Visit(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, right), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> Equal<T, TType>(this Expression<Func<T, TType>> expr,TType val) {
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(expr.Body,Expression.Constant(val)),expr.Parameters);
        }

        public static Expression<Func<T, bool>> In<T, TType>(this Expression<Func<T, TType>> expr, TType[] vals)
        {
            var method = typeof(System.Linq.Enumerable).GetMethods().Where(a => a.Name == "Contains" && a.GetParameters().Length == 2).FirstOrDefault().MakeGenericMethod(typeof(TType));
            return Expression.Lambda<Func<T, bool>>(Expression.Call(null, method, Expression.Constant(vals), expr.Body),expr.Parameters);
        }
        public static Expression<Func<T, bool>> Like<T>(this Expression<Func<T, string>> expr, string val)
        {
            var method = typeof(string).GetMethod("Contains");
            return Expression.Lambda<Func<T, bool>>(Expression.Call(expr.Body, method, Expression.Constant(val)), expr.Parameters);
        }
        public static Expression<Func<T,TResult>> DotExpression<T, TResult>(string name)
        {
            var paramExpr = Expression.Parameter(typeof(T), "a");
            return (Expression<Func<T,TResult>>)Expression.Lambda(Expression.MakeMemberAccess(paramExpr, typeof(T).GetMember(name,
                BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty
                ).FirstOrDefault()), paramExpr);
        }

        private class ExParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;

            private readonly ParameterExpression _newParameter;

            public ExParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (node == _oldParameter)
                {
                    return _newParameter;
                }

                return base.VisitParameter(node);
            }
        }
    }
}