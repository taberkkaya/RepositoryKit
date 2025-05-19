using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericRepository;

/// <summary>
/// Generic repository implementation using Entity Framework Core DbContext.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TContext">The DbContext type.</typeparam>
public class Repository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    private readonly TContext _context;
    private DbSet<TEntity> Entity;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity, TContext}"/> class.
    /// </summary>
    /// <param name="context">The DbContext instance.</param>
    public Repository(TContext context)
    {
        _context = context;
        Entity = _context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public void Add(TEntity entity)
    {
        Entity.Add(entity);
    }

    /// <inheritdoc/>
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entity.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Entity.AddRangeAsync(entities, cancellationToken);
    }

    /// <inheritdoc/>
    public bool Any(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.Any(expression);
    }

    /// <inheritdoc/>
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Entity.AnyAsync(expression, cancellationToken);
    }

    /// <inheritdoc/>
    public void Delete(TEntity entity)
    {
        Entity.Remove(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        Entity.Remove(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteByIdAsync(string id)
    {
        TEntity entity = await Entity.FindAsync(id);
        Entity.Remove(entity);
    }

    /// <inheritdoc/>
    public void DeleteRange(ICollection<TEntity> entities)
    {
        Entity.RemoveRange(entities);
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> GetAll()
    {
        return Entity.AsNoTracking().AsQueryable();
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> GetAllWithTracking()
    {
        return Entity.AsQueryable();
    }

    /// <inheritdoc/>
    public TEntity GetByExpression(Expression<Func<TEntity, bool>> expression)
    {
        TEntity entity = Entity.Where(expression).AsNoTracking().FirstOrDefault();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc/>
    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, bool isTrackingActive = true)
    {
        TEntity entity;
        if (isTrackingActive)
        {
            entity = await Entity.Where(expression).FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            entity = await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        return entity;
    }

    /// <inheritdoc/>
    public TEntity GetByExpressionWithTracking(Expression<Func<TEntity, bool>> expression)
    {
        TEntity entity = Entity.Where(expression).FirstOrDefault();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByExpressionWithTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.Where(expression).FirstOrDefaultAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc/>
    public TEntity GetFirst()
    {
        TEntity entity = Entity.AsNoTracking().FirstOrDefault();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetFirstAsync(CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.AsNoTracking().Where(expression).AsQueryable();
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> WhereWithTracking(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.Where(expression).AsQueryable();
    }

    /// <inheritdoc/>
    public void Update(TEntity entity)
    {
        Entity.Update(entity);
    }

    /// <inheritdoc/>
    public void UpdateRange(ICollection<TEntity> entities)
    {
        Entity.UpdateRange(entities);
    }

    /// <inheritdoc/>
    public void AddRange(ICollection<TEntity> entities)
    {
        Entity.AddRange(entities);
    }

    /// <inheritdoc/>
    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTrackingActive = true)
    {
        if (isTrackingActive)
        {
            return Entity.FirstOrDefault(expression);
        }

        return Entity.AsNoTracking().FirstOrDefault(expression);
    }

    /// <inheritdoc/>
    public IQueryable<KeyValuePair<bool, int>> CountBy(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Entity.CountBy(expression);
    }

    /// <inheritdoc/>
    public TEntity First(Expression<Func<TEntity, bool>> expression, bool isTrackingActive = true)
    {
        if (isTrackingActive)
        {
            return Entity.First(expression);
        }

        return Entity.AsNoTracking().First(expression);
    }

    /// <inheritdoc/>
    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, bool isTrackingActive = true)
    {
        if (isTrackingActive)
        {
            return await Entity.FirstAsync(expression, cancellationToken);
        }

        return await Entity.AsNoTracking().FirstAsync(expression, cancellationToken);
    }
}
