using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> source, string property, object search)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (search == null) return source;

            var lambda = property.GetLambdaExpression<T>();
            if (lambda == null) return source;

            Type propertyType = lambda.Body.Type;

            var valueTyped = search.TryChangeType(propertyType);
            if (!valueTyped.IsSuccess) return source;

            var compareName = propertyType != typeof(string) ? "Equals" : "Contains";
            var compare = propertyType.GetMethod(compareName, new[] { propertyType });

            var valueExpression = Expression.Constant(valueTyped.Value);
            var condition = Expression.Call(lambda.GetBody(), compare, valueExpression);

            var predicate = Expression.Lambda<Func<T, bool>>(condition, lambda.Parameters.ToArray());
            return source.Where(predicate);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> source, string propertyLeft, string propertyRight, object search)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (search == null) return source;

            var paramExpression = Expression.Parameter(typeof(T), "x");
            var lambda = propertyLeft.GetLambdaExpression<T>(paramExpression);
            if (lambda == null) return source;

            var lambdaRight = propertyRight.GetLambdaExpression<T>(paramExpression);
            if (lambdaRight == null) return source;

            Type propertyType = lambda.Body.Type;

            var valueTyped = search.TryChangeType(propertyType);
            if (!valueTyped.IsSuccess) return source;

            var compareName = propertyType != typeof(string) ? "Equals" : "Contains";
            var compare = propertyType.GetMethod(compareName, new[] { propertyType });

            var valueExpression = Expression.Constant(valueTyped.Value);
            var condition = Expression.Call(lambda.GetBody(), compare, valueExpression);
            var conditionRight = Expression.Call(lambdaRight.GetBody(), compare, valueExpression);

            var conditionOr = Expression.OrElse(condition, conditionRight);

            var predicate = Expression.Lambda<Func<T, bool>>(conditionOr, paramExpression);
            return source.Where(predicate);
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> source, string[] filters)
        {
            IQueryable<T> target = source;
            foreach (var term in filters)
            {
                var pairs = term.Split("|");
                var isPair = pairs.Length == 2 && pairs[0].Trim().Length > 0 && pairs[1].Trim().Length > 0;

                if (isPair)
                {
                    var key = pairs[0];
                    var value = pairs[1];

                    var columns = key.Split(',');
                    var isMultiColumns = columns.Length == 2 && columns[0].Trim().Length > 0 && columns[1].Trim().Length > 0;

                    if (isMultiColumns)
                        target = target.Where(columns[0], columns[1], pairs[1]);
                    else
                        target = target.Where(pairs[0], pairs[1]);
                }
            }
            return target;
        }

        static Expression GetBody(this LambdaExpression expression)
        {
            Expression body;
            if (expression.Body is UnaryExpression)
                body = ((UnaryExpression)expression.Body).Operand;
            else
                body = expression.Body;

            return body;
        }

        static LambdaExpression GetLambdaExpression<T>(this string property, ParameterExpression parameterExpression = null)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = parameterExpression ?? Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (expr != null && pi != null)
                {
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
                else
                    expr = null;
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            return expr == null ? null : Expression.Lambda(delegateType, expr, arg);
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

    }
}
