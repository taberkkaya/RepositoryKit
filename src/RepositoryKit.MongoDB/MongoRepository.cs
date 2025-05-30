using MongoDB.Driver;
using RepositoryKit.Core;
using System.Linq.Expressions;

namespace RepositoryKit.MongoDB;

/// <summary>
/// MongoDB implementation of the repository pattern
/// </summary>
public class MongoRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> _collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TEntity>(collectionName);
    }

    public override async Task<TEntity?> GetByIdAsync(TKey id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public override async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public override async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public override async Task AddAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public override async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _collection.InsertManyAsync(entities);
    }

    public override async Task UpdateAsync(TEntity entity)
    {
        var id = GetIdValue(entity);
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public override async Task RemoveAsync(TEntity entity)
    {
        var id = GetIdValue(entity);
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }

    public override async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        var ids = entities.Select(GetIdValue);
        var filter = Builders<TEntity>.Filter.In("_id", ids);
        await _collection.DeleteManyAsync(filter);
    }

    public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _collection.Find(predicate).AnyAsync();
    }

    public override async Task<int> CountAsync()
    {
        return (int)await _collection.CountDocumentsAsync(_ => true);
    }

    public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return (int)await _collection.CountDocumentsAsync(predicate);
    }

    private TKey GetIdValue(TEntity entity)
    {
        var property = typeof(TEntity).GetProperty("Id") ?? typeof(TEntity).GetProperty("ID");
        if (property == null)
        {
            throw new InvalidOperationException("Entity must have an Id or ID property.");
        }
        return (TKey)property.GetValue(entity)!;
    }
}