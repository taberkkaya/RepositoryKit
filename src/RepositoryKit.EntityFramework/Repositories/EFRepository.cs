// EFRepository.cs
using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core.Interfaces;
using System.Linq.Expressions;

namespace RepositoryKit.EntityFramework.Repositories;

public class EFRepository<T, TKey> :
    IRepository<T, TKey>,
    IRepositoryQuery<T, TKey>,
    IRepositoryBulk<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public EFRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task<T?> GetByIdAsync(TKey id, bool tracking, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet;
        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => EF.Property<TKey>(e, "Id")!.Equals(id), cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool tracking, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet;
        if (!tracking)
            query = query.AsNoTracking();

        return await query.Where(predicate).ToListAsync(cancellationToken);
    }

    public IQueryable<T> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<IEnumerable<T>> GetSortedAsync<TSortKey>(Expression<Func<T, TSortKey>> orderBy, bool descending = false, CancellationToken cancellationToken = default)
    {
        var query = descending ? _dbSet.OrderByDescending(orderBy) : _dbSet.OrderBy(orderBy);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.UpdateRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }
}