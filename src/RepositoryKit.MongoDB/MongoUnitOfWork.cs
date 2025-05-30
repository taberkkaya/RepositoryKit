using MongoDB.Driver;
using RepositoryKit.Core;

namespace RepositoryKit.MongoDB;

/// <summary>
/// MongoDB implementation of Unit of Work pattern
/// </summary>
public class MongoUnitOfWork : IUnitOfWork
{
    private readonly IMongoDatabase _database;
    private readonly Dictionary<Type, object> _repositories;
    private IClientSessionHandle? _session;

    public MongoUnitOfWork(IMongoDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (_repositories.ContainsKey(type))
        {
            return (IRepository<TEntity, TKey>)_repositories[type];
        }

        var collectionName = type.Name.EndsWith("s") ? type.Name : type.Name + "s";
        var repository = new MongoRepository<TEntity, TKey>(_database, collectionName);
        _repositories.Add(type, repository);
        return repository;
    }

    public async Task<int> CommitAsync()
    {
        _session ??= await _database.Client.StartSessionAsync();
        _session.StartTransaction();

        try
        {
            await _session.CommitTransactionAsync();
            return 1; // MongoDB doesn't return affected count
        }
        catch
        {
            await _session.AbortTransactionAsync();
            throw;
        }
    }

    public async Task RollbackAsync()
    {
        if (_session != null && _session.IsInTransaction)
        {
            await _session.AbortTransactionAsync();
        }
    }

    public void Dispose()
    {
        _session?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (_session != null)
        {
            _session.Dispose();
        }
        GC.SuppressFinalize(this);
        await Task.CompletedTask;
    }
}