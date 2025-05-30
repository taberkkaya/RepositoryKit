namespace RepositoryKit.Core;

/// <summary>
/// Unit of Work pattern interface to manage transactions and multiple repositories.
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets a repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key</typeparam>
    /// <returns>An instance of the repository</returns>
    IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;

    /// <summary>
    /// Commits all changes made in this unit of work.
    /// </summary>
    /// <returns>The number of affected records</returns>
    Task<int> CommitAsync();

    /// <summary>
    /// Rolls back all changes made in this unit of work.
    /// </summary>
    Task RollbackAsync();
}