using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core;

namespace RepositoryKit.EntityFramework;

/// <summary>
/// Entity Framework implementation of the Unit of Work pattern.
/// Manages database transactions and coordinates the work of multiple repositories.
/// </summary>
public class EFUnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private bool _disposed;
    private Dictionary<Type, object> _repositories;

    public EFUnitOfWork(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IRepository<TEntity, TKey>)_repositories[typeof(TEntity)];
        }

        var repository = new EFRepository<TEntity, TKey>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public Task RollbackAsync()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_context != null)
        {
            await _context.DisposeAsync().ConfigureAwait(false);
        }
    }
}