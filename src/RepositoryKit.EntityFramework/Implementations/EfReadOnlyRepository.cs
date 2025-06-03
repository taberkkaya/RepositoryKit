// File: src/RepositoryKit.EntityFramework/Implementations/EfReadOnlyRepository.cs

namespace RepositoryKit.EntityFramework.Implementations;

using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core.Exceptions;
using RepositoryKit.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Entity Framework Core implementation of <see cref="IReadOnlyRepository{TEntity}"/> for a given DbContext type.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TContext">DbContext type.</typeparam>
public class EfReadOnlyRepository<TEntity, TContext> : IReadOnlyRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of <see cref="EfReadOnlyRepository{TEntity, TContext}"/>.
    /// </summary>
    /// <param name="context">The DbContext instance.</param>
    public EfReadOnlyRepository(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    /// <inheritdoc />
    public IQueryable<TEntity> Query() => _dbSet.AsQueryable();

    /// <inheritdoc />
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(
                "An error occurred while getting an entity.",
                RepositoryErrorType.Query,
                typeof(TEntity),
                nameof(GetAsync),
                ex
            );
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        try
        {
            if (predicate is null)
                return await _dbSet.ToListAsync(cancellationToken);
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(
                "An error occurred while getting entities.",
                RepositoryErrorType.Query,
                typeof(TEntity),
                nameof(GetAllAsync),
                ex
            );
        }
    }
}
