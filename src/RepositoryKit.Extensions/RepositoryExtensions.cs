using System.Linq.Expressions;
using RepositoryKit.Core;

namespace RepositoryKit.Extensions;

public static class RepositoryExtensions
{
    public static async Task<PagedResult<TEntity>> GetPagedAsync<TEntity, TKey>(
        this IReadOnlyRepository<TEntity, TKey> repository,
        int pageNumber,
        int pageSize) where TEntity : class
    {
        var count = await repository.CountAsync();
        var items = await repository.GetAllAsync();

        items = items
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<TEntity>(items, count, pageNumber, pageSize);
    }

    public static async Task<PagedResult<TEntity>> GetPagedAsync<TEntity, TKey>(
        this IReadOnlyRepository<TEntity, TKey> repository,
        Expression<Func<TEntity, bool>> predicate,
        int pageNumber,
        int pageSize) where TEntity : class
    {
        var count = await repository.CountAsync(predicate);
        var items = await repository.FindAsync(predicate);

        items = items
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<TEntity>(items, count, pageNumber, pageSize);
    }

    public static async Task<IEnumerable<TEntity>> GetSortedAsync<TEntity, TKey>(
        this IReadOnlyRepository<TEntity, TKey> repository,
        Expression<Func<TEntity, object>> sortExpression,
        SortDirection sortDirection = SortDirection.Ascending) where TEntity : class
    {
        var items = await repository.GetAllAsync();

        return sortDirection == SortDirection.Ascending
            ? items.OrderBy(sortExpression.Compile())
            : items.OrderByDescending(sortExpression.Compile());
    }
}

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public enum SortDirection
{
    Ascending,
    Descending
}