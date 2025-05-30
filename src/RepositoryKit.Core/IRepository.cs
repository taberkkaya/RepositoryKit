using System.Linq.Expressions;

namespace RepositoryKit.Core;

/// <summary>
/// Base interface for repository pattern.
/// Defines common CRUD operations for entities.
/// </summary>
/// <typeparam name="TEntity">The entity type</typeparam>
/// <typeparam name="TKey">The type of the entity's primary key</typeparam>
public interface IRepository<TEntity, in TKey> where TEntity : class
{
    /// <summary>
    /// Gets an entity by its primary key.
    /// </summary>
    /// <param name="id">The primary key value</param>
    /// <returns>The entity if found, otherwise null</returns>
    Task<TEntity?> GetByIdAsync(TKey id);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A list of all entities</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Finds entities based on a predicate.
    /// </summary>
    /// <param name="predicate">The filter condition</param>
    /// <returns>A list of matching entities</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Adds multiple entities to the repository.
    /// </summary>
    /// <param name="entities">The entities to add</param>
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove</param>
    Task RemoveAsync(TEntity entity);

    /// <summary>
    /// Removes multiple entities from the repository.
    /// </summary>
    /// <param name="entities">The entities to remove</param>
    Task RemoveRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Checks if any entity matches the predicate.
    /// </summary>
    /// <param name="predicate">The filter condition</param>
    /// <returns>True if any match is found, otherwise false</returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Counts all entities.
    /// </summary>
    /// <returns>The total count of entities</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Counts entities matching the predicate.
    /// </summary>
    /// <param name="predicate">The filter condition</param>
    /// <returns>The count of matching entities</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
}