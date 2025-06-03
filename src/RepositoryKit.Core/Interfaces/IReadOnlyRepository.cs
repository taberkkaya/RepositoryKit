namespace RepositoryKit.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides read-only operations for the specified entity type.
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public interface IReadOnlyRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Returns an <see cref="IQueryable{TEntity}"/> for advanced query scenarios.
    /// </summary>
    IQueryable<TEntity> Query();

    /// <summary>
    /// Asynchronously gets a single entity matching the given predicate.
    /// </summary>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously gets all entities matching the given predicate, or all if predicate is null.
    /// </summary>
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
}
