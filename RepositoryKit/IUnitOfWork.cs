namespace GenericRepository;

/// <summary>
/// Represents a unit of work that manages database transaction scope.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Asynchronously saves all changes made in the context.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all changes made in the context.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    int SaveChanges();
}
