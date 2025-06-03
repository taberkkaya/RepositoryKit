// File: RepositoryKit.Extensions/Extensions/IQueryableExtensions.cs

namespace RepositoryKit.Extensions.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

/// <summary>
/// Provides extension methods for <see cref="IQueryable{T}"/> sequences.
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// Returns a paged subset of a queryable source as a list.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="source">The queryable source.</param>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="pageSize">The size of a page.</param>
    /// <returns>A list containing the items in the specified page.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If page or pageSize is less than 1.</exception>
    public static List<T> ToPagedList<T>(this IQueryable<T> source, int page, int pageSize)
    {
        if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
        if (pageSize < 1) throw new ArgumentOutOfRangeException(nameof(pageSize));
        return source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    /// <summary>
    /// Dynamically sorts the queryable by property name (using reflection).
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="source">The queryable source.</param>
    /// <param name="propertyName">Property name to sort by.</param>
    /// <param name="descending">Sort direction, descending if true.</param>
    /// <returns>Sorted queryable.</returns>
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, string propertyName, bool descending = false)
    {
        if (string.IsNullOrEmpty(propertyName)) return source;
        var param = Expression.Parameter(typeof(T));
        var property = Expression.PropertyOrField(param, propertyName);
        var lambda = Expression.Lambda(property, param);

        var methodName = descending ? "OrderByDescending" : "OrderBy";
        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.Type);

        return (IQueryable<T>)method.Invoke(null, new object[] { source, lambda })!;
    }

    /// <summary>
    /// Applies the predicate if it is not null.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="source">The queryable source.</param>
    /// <param name="predicate">Optional filter predicate.</param>
    /// <returns>Filtered or original queryable.</returns>
    public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> source, Expression<Func<T, bool>>? predicate)
    {
        return predicate == null ? source : source.Where(predicate);
    }

    /// <summary>
    /// Projects each element of a sequence into a new form.
    /// </summary>
    /// <typeparam name="TSource">Element type of source.</typeparam>
    /// <typeparam name="TResult">Projection type.</typeparam>
    /// <param name="source">The queryable source.</param>
    /// <param name="selector">Projection expression.</param>
    /// <returns>Projected queryable.</returns>
    public static IQueryable<TResult> SelectAs<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
    {
        return source.Select(selector);
    }

    /// <summary>
    /// Returns the first element or default value if sequence is empty.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="source">The queryable source.</param>
    /// <returns>First element or default value.</returns>
    public static T? FirstOrNone<T>(this IQueryable<T> source)
    {
        return source.FirstOrDefault();
    }
}
