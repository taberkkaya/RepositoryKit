namespace RepositoryKit.Core.Interfaces;

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides CRUD operations for the specified entity type.
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Asynchronously adds a new entity.
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing entity.
    /// </summary>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes an entity.
    /// </summary>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
