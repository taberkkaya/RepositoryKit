namespace RepositoryKit.MongoDb.Implementations;

using MongoDB.Driver;
using RepositoryKit.Core.Interfaces;
using System.Linq.Expressions;

/// <summary>
/// MongoDb implementation of CRUD repository for a given entity.
/// </summary>
public class MongoRepository<TEntity, TContext> : MongoReadOnlyRepository<TEntity, TContext>, IRepository<TEntity>
    where TEntity : class
    where TContext : class, IMongoDatabase
{
    public MongoRepository(TContext database) : base(database) { }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var id = entity.GetType().GetProperty("Id")?.GetValue(entity);
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var id = entity.GetType().GetProperty("Id")?.GetValue(entity);
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }
}
