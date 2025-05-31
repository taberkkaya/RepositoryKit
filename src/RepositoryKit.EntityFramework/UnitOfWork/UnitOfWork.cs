// UnitOfWork.cs
using Microsoft.EntityFrameworkCore;

namespace RepositoryKit.EntityFramework.UnitOfWork;

/// <summary>
/// Provides a unit of work implementation for EF Core.
/// </summary>
public class UnitOfWork : IAsyncDisposable
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Commits all changes made in a transaction.
    /// </summary>
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Rolls back any tracked changes.
    /// </summary>
    public void Rollback()
    {
        foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
        {
            entry.State = EntityState.Detached;
        }
    }

    /// <summary>
    /// Disposes the DbContext.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}
