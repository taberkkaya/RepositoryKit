// IQueryableExtensions.cs
using System.Linq.Expressions;

namespace RepositoryKit.Extensions;

/// <summary>
/// Provides extension methods for IQueryable.
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// Applies pagination to a query.
    /// </summary>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
    {
        return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    /// <summary>
    /// Applies ordering to a query.
    /// </summary>
    public static IQueryable<T> OrderByField<T>(this IQueryable<T> query, Expression<Func<T, object>> orderBy, bool descending = false)
    {
        return descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
    }
}
