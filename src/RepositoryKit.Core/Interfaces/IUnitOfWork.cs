﻿namespace RepositoryKit.Core.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides Unit of Work pattern for a specific DbContext type. Used to coordinate repository and save operations.
    /// </summary>
    /// <typeparam name="TContext">The DbContext type.</typeparam>
    public interface IUnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : class
    {
        /// <summary>
        /// Returns a repository instance for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }

    /// <summary>
    /// Provides Unit of Work abstraction for application layers without DbContext dependency.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the underlying database.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Synchronously saves all changes made in this unit of work to the underlying database.
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Returns a repository instance for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
