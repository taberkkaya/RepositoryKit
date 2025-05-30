using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepositoryKit.Core;

namespace RepositoryKit.EntityFramework;

/// <summary>
/// Provides Entity Framework specific extension methods for IServiceCollection.
/// </summary>
public static class EntityFrameworkServiceCollectionExtensions
{
    public static IServiceCollection AddRepositoryKitWithEntityFramework<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.AddScoped<IUnitOfWork, EFUnitOfWork>(provider =>
            new EFUnitOfWork(provider.GetRequiredService<TDbContext>()));

        return services;
    }
}