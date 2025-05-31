// IRepositoryQuery.cs (final - generic TKey destekli versiyon)
using System.Linq.Expressions;

namespace RepositoryKit.Core.Interfaces;

/// <summary>
/// Extended repository query operations with TKey support.
/// </summary>
public interface IRepositoryQuery<T, TKey> where T : class
{
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool tracking, CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(TKey id, bool tracking, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetSortedAsync<TSortKey>(Expression<Func<T, TSortKey>> orderBy, bool descending = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

    IQueryable<T> AsQueryable();
}
