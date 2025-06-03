using Microsoft.EntityFrameworkCore;

namespace RepositoryKit.Core.Interfaces;

/// <summary>
/// Manages UnitOfWork instances for multiple DbContext types.
/// </summary>
public interface IUnitOfWorkManager
{
    /// <summary>
    /// Returns a UnitOfWork instance for the specified DbContext type.
    /// </summary>
    /// <typeparam name="TContext">DbContext type</typeparam>
    IUnitOfWork<TContext> GetUnitOfWork<TContext>() where TContext : DbContext;
}
