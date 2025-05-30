using System.Linq.Expressions;

namespace RepositoryKit.Core;

/// <summary>
/// Abstract base class for repository implementations.
/// Provides common CRUD operations that can be inherited by specific implementations.
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <typeparam name="TKey">The type of the entity's primary key</typeparam>
public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    public abstract Task<TEntity?> GetByIdAsync(TKey id);
    public abstract Task<IEnumerable<TEntity>> GetAllAsync();
    public abstract Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    public abstract Task AddAsync(TEntity entity);
    public abstract Task AddRangeAsync(IEnumerable<TEntity> entities);
    public abstract Task UpdateAsync(TEntity entity);
    public abstract Task RemoveAsync(TEntity entity);
    public abstract Task RemoveRangeAsync(IEnumerable<TEntity> entities);
    public abstract Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    public abstract Task<int> CountAsync();
    public abstract Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
}