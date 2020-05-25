using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ust.Api.Common.ExpressionFilter
{
    //Можно использовать только сравнение на равно и Contains у стринга
    public static class ExpressionExtension<T>
    {
        public static IQueryable<T> BuildFilter(IQueryable<T> queryableData, Dictionary<string, object> filterDictionary)
        {
            MethodCallExpression whereCallExpression = null;
            ParameterExpression pe = Expression.Parameter(typeof(T), "data");
            Expression previuos = null;
            Expression predicateBody = null;

            foreach (var filter in filterDictionary)
            {
                if (filter.Value == null)
                    continue;

                if (filter.Key == "Title")
                {
                    var entityPropertyForString = Expression.Property(pe, typeof(T).GetProperty(filter.Key));
                    var entryValue = Expression.Constant(filter.Value.ToString().ToUpper());
                    var containsCall = Expression.Call(
                        Expression.Call(
                           entityPropertyForString,
                            "ToUpper", Type.EmptyTypes),
                        "Contains",
                        Type.EmptyTypes,
                        entryValue
                    );

                    if (previuos != null)
                    {
                        predicateBody = Expression.AndAlso(previuos, containsCall);
                    }
                    previuos = predicateBody ?? containsCall;

                    continue;
                }

                var entityProperty = Expression.Property(pe, typeof(T).GetProperty(filter.Key));
                var comparisonValue = Expression.Constant(filter.Value);
                Expression whereCondidtional = Expression.Equal(entityProperty, comparisonValue);
                if (previuos != null)
                {
                    predicateBody = Expression.AndAlso(previuos, whereCondidtional);
                }
                previuos = predicateBody ?? whereCondidtional;
            }

            if (previuos == null)
            {
                return queryableData;
            }

            whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { queryableData.ElementType },
                queryableData.Expression,
                Expression.Lambda<Func<T, bool>>(predicateBody ?? previuos, new ParameterExpression[] { pe }));

            return queryableData.Provider.CreateQuery<T>(whereCallExpression);
        }

    }

    public static class FilterBuilder<T>
    {
        public static Dictionary<string, object> BuildFilterDictionary(T filter)
        {
            Dictionary<string, object> filterDictionary = new Dictionary<string, object>();

            var type = filter.GetType();
            foreach (var prop in type.GetProperties())
            {
                filterDictionary.Add(prop.Name, prop.GetValue(filter));
            }

            return filterDictionary;
        }
    }
}
