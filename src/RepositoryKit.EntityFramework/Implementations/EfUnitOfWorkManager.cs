// File: src/RepositoryKit.EntityFramework/Implementations/EfUnitOfWorkManager.cs

namespace RepositoryKit.EntityFramework.Implementations;

using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core.Interfaces;
using System;
using System.Collections.Concurrent;

/// <summary>
/// Entity Framework Core implementation of <see cref="IUnitOfWorkManager"/>, manages UnitOfWork instances for multiple DbContext types.
/// </summary>
public class EfUnitOfWorkManager : IUnitOfWorkManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<Type, object> _uowCache = new();

    /// <summary>
    /// Initializes a new instance of <see cref="EfUnitOfWorkManager"/>.
    /// </summary>
    /// <param name="serviceProvider">The DI service provider instance.</param>
    public EfUnitOfWorkManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <inheritdoc />
    public IUnitOfWork<TContext> GetUnitOfWork<TContext>() where TContext : DbContext
    {
        return (IUnitOfWork<TContext>)_uowCache.GetOrAdd(typeof(TContext), _ =>
        {
            var context = (TContext)_serviceProvider.GetService(typeof(TContext))!;
            if (context == null)
                throw new InvalidOperationException($"No service for type '{typeof(TContext).FullName}' has been registered.");
            return new EfUnitOfWork<TContext>(context);
        });
    }
}
