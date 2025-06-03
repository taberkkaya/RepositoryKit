namespace RepositoryKit.MongoDb.Implementations;

using MongoDB.Driver;
using RepositoryKit.Core.Interfaces;
using System.Linq.Expressions;

/// <summary>
/// MongoDb implementation of read-only repository for a given entity.
/// </summary>
public class MongoReadOnlyRepository<TEntity, TContext> : IReadOnlyRepository<TEntity>
    where TEntity : class
    where TContext : class, IMongoDatabase
{
    protected readonly TContext _database;
    protected readonly IMongoCollection<TEntity> _collection;

    public MongoReadOnlyRepository(TContext database)
    {
        _database = database;
        _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public IQueryable<TEntity> Query() => _collection.AsQueryable();

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await Task.FromResult(_collection.AsQueryable().FirstOrDefault(predicate));

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        => await Task.FromResult(predicate == null
                ? _collection.AsQueryable().ToList()
                : _collection.AsQueryable().Where(predicate).ToList());
}
