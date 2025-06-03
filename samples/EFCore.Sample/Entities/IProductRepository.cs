// File: EFCore.Sample/Entities/IProductRepository.cs

using RepositoryKit.Core.Interfaces;
using EFCore.Sample.Entities;

namespace EFCore.Sample.Entities;

/// <summary>
/// Interface for ProductRepository, includes CRUD and custom business methods.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Gets products with price above the given threshold.
    /// </summary>
    Task<List<Product>> GetExpensiveProductsAsync(decimal minPrice);
}
