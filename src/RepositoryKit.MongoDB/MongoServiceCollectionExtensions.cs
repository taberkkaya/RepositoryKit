using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RepositoryKit.Core;

namespace RepositoryKit.MongoDB;

public static class MongoServiceCollectionExtensions
{
    public static IServiceCollection AddRepositoryKitWithMongoDB(
        this IServiceCollection services,
        string connectionString,
        string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        services.AddSingleton<IMongoDatabase>(database);
        services.AddScoped<IUnitOfWork, MongoUnitOfWork>(provider =>
            new MongoUnitOfWork(provider.GetRequiredService<IMongoDatabase>()));

        return services;
    }
}