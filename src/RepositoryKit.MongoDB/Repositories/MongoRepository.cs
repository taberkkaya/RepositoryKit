// MongoRepository.cs
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryKit.Core.Interfaces;
using System.Linq.Expressions;

namespace RepositoryKit.MongoDB.Repositories;

public class MongoRepository<T, TKey> :
    IRepository<T, TKey>,
    IRepositoryQuery<T, TKey>,
    IRepositoryBulk<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<T?> GetByIdAsync(TKey id, bool tracking, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return _collection.Find(filter).FirstOrDefaultAsync(cancellationToken); // tracking ignored
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.AsQueryable().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool tracking, CancellationToken cancellationToken = default)
    {
        return await _collection.AsQueryable().Where(predicate).ToListAsync(cancellationToken); // tracking ignored
    }

    public IQueryable<T> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public async Task<IEnumerable<T>> GetSortedAsync<TSortKey>(Expression<Func<T, TSortKey>> orderBy, bool descending = false, CancellationToken cancellationToken = default)
    {
        var query = _collection.AsQueryable();
        query = descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
        return await Task.FromResult(query.ToList());
    }

    public async Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _collection.AsQueryable()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var id = entity.GetType().GetProperty("Id")?.GetValue(entity);
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        var id = entity.GetType().GetProperty("Id")?.GetValue(entity);
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _collection.InsertManyAsync(entities, cancellationToken: cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            var id = entity.GetType().GetProperty("Id")?.GetValue(entity);
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            var id = entity.GetType().GetProperty("Id")?.GetValue(entity);
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.DeleteOneAsync(filter, cancellationToken);
        }
    }
}