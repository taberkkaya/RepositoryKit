using System.Linq.Expressions;

namespace GenericRepository;

/// <summary>
/// Generic repository interface defining common data access methods for entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Gets all entities as a queryable without tracking.
    /// </summary>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Gets all entities as a queryable with tracking enabled.
    /// </summary>
    IQueryable<TEntity> GetAllWithTracking();

    /// <summary>
    /// Filters entities by the given predicate without tracking.
    /// </summary>
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Filters entities by the given predicate with tracking enabled.
    /// </summary>
    IQueryable<TEntity> WhereWithTracking(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Returns the first entity matching the predicate with optional tracking.
    /// Throws if no entity is found.
    /// </summary>
    TEntity First(Expression<Func<TEntity, bool>> expression, bool isTrackingActive = true);

    /// <summary>
    /// Returns the first entity matching the predicate with optional tracking,
    /// or default if none found.
    /// </summary>
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTrackingActive = true);

    /// <summary>
    /// Asynchronously returns the first entity matching the predicate with optional tracking,
    /// or default if none found.
    /// </summary>
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, bool isTrackingActive = true);

    /// <summary>
    /// Asynchronously returns the first entity matching the predicate with optional tracking.
    /// Throws if no entity is found.
    /// </summary>
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, bool isTrackingActive = true);

    /// <summary>
    /// Asynchronously gets an entity by the given predicate without tracking.
    /// </summary>
    Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets an entity by the given predicate with tracking.
    /// </summary>
    Task<TEntity> GetByExpressionWithTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously returns the first entity without tracking.
    /// </summary>
    Task<TEntity> GetFirstAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously determines if any entity matches the given predicate.
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines if any entity matches the given predicate.
    /// </summary>
    bool Any(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Gets an entity by the given predicate without tracking.
    /// </summary>
    TEntity GetByExpression(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Gets an entity by the given predicate with tracking.
    /// </summary>
    TEntity GetByExpressionWithTracking(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Returns the first entity without tracking.
    /// </summary>
    TEntity GetFirst();

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    void Add(TEntity entity);

    /// <summary>
    /// Adds multiple entities asynchronously.
    /// </summary>
    Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds multiple entities.
    /// </summary>
    void AddRange(ICollection<TEntity> entities);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    void Update(TEntity entity);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    void UpdateRange(ICollection<TEntity> entities);

    /// <summary>
    /// Deletes an entity by its identifier asynchronously.
    /// </summary>
    Task DeleteByIdAsync(string id);

    /// <summary>
    /// Deletes entities matching the given predicate asynchronously.
    /// </summary>
    Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the given entity.
    /// </summary>
    void Delete(TEntity entity);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    void DeleteRange(ICollection<TEntity> entities);

    /// <summary>
    /// Counts entities grouped by a boolean key defined by the predicate.
    /// </summary>
    IQueryable<KeyValuePair<bool, int>> CountBy(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
}
