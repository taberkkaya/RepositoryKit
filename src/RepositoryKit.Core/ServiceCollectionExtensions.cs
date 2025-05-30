using Microsoft.Extensions.DependencyInjection;
namespace RepositoryKit.Core;

/// <summary>
/// Provides extension methods for IServiceCollection to register repository services.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositoryKit<TUnitOfWork>(this IServiceCollection services)
        where TUnitOfWork : class, IUnitOfWork
    {
        services.AddScoped<IUnitOfWork, TUnitOfWork>();
        return services;
    }

    public static IServiceCollection AddRepository<TEntity, TKey, TRepository>(this IServiceCollection services)
        where TEntity : class
        where TRepository : class, IRepository<TEntity, TKey>
    {
        services.AddScoped<IRepository<TEntity, TKey>, TRepository>();
        return services;
    }
}