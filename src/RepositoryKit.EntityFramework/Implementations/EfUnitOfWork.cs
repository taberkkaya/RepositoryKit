namespace RepositoryKit.EntityFramework.Implementations;

using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core.Exceptions;
using RepositoryKit.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Entity Framework Core implementation of <see cref="IUnitOfWork{TContext}"/> for a given DbContext type.
/// </summary>
/// <typeparam name="TContext">DbContext type.</typeparam>
public class EfUnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of <see cref="EfUnitOfWork{TContext}"/>.
    /// </summary>
    /// <param name="context">The DbContext instance.</param>
    public EfUnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        return (IRepository<TEntity>)_repositories.GetOrAdd(
            typeof(TEntity),
            _ => new EfRepository<TEntity, TContext>(_context)
        );
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new RepositoryException(
                "A database update error occurred while saving changes.",
                RepositoryErrorType.SaveChanges,
                typeof(TContext),
                nameof(SaveChangesAsync),
                ex
            );
        }
        catch (Exception ex)
        {
            throw new RepositoryException(
                "An error occurred while saving changes.",
                RepositoryErrorType.SaveChanges,
                typeof(TContext),
                nameof(SaveChangesAsync),
                ex
            );
        }
    }

    /// <inheritdoc />
    public int SaveChanges()
    {
        try
        {
            return _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new RepositoryException(
                "A database update error occurred while saving changes.",
                RepositoryErrorType.SaveChanges,
                typeof(TContext),
                nameof(SaveChanges),
                ex
            );
        }
        catch (Exception ex)
        {
            throw new RepositoryException(
                "An error occurred while saving changes.",
                RepositoryErrorType.SaveChanges,
                typeof(TContext),
                nameof(SaveChanges),
                ex
            );
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
                _repositories.Clear();
            }
            _disposed = true;
        }
    }
}
