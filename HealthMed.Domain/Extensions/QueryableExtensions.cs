using System.Linq.Expressions;

namespace HealthMed.Domain.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByProperty, bool desc)
    {
        var command = desc ? "OrderByDescending" : "OrderBy";
        var type = typeof(T);
        var property = type.GetProperty(orderByProperty);
        if (property == null)
        {
            throw new ArgumentException("Property not found", nameof(orderByProperty));
        }
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
            source.Expression, Expression.Quote(orderByExpression));

        return source.Provider.CreateQuery<T>(resultExpression);
    }
}
