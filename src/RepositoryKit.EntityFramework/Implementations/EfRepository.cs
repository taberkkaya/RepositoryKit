// File: src/RepositoryKit.EntityFramework/Implementations/EfRepository.cs

namespace RepositoryKit.EntityFramework.Implementations;

using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core.Exceptions;
using RepositoryKit.Core.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Entity Framework Core implementation of <see cref="IRepository{TEntity}"/> for a given DbContext type.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TContext">DbContext type.</typeparam>
public class EfRepository<TEntity, TContext> : EfReadOnlyRepository<TEntity, TContext>, IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of <see cref="EfRepository{TEntity, TContext}"/>.
    /// </summary>
    /// <param name="context">The DbContext instance.</param>
    public EfRepository(TContext context) : base(context) { }

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        try
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(
                "A database update error occurred while adding an entity.",
                RepositoryErrorType.Add,
                typeof(TEntity),
                nameof(AddAsync),
                ex
            );
        }
    }

    /// <inheritdoc />
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        try
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw new RepositoryException(
                "A database update error occurred while updating an entity.",
                RepositoryErrorType.Update,
                typeof(TEntity),
                nameof(UpdateAsync),
                ex
            );
        }
    }

    /// <inheritdoc />
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        try
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw new RepositoryException(
                "A database update error occurred while deleting an entity.",
                RepositoryErrorType.Delete,
                typeof(TEntity),
                nameof(DeleteAsync),
                ex
            );
        }
    }
}
