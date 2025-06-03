using RepositoryKit.Core.Interfaces;

namespace MongoDB.Sample.Models;

/// <summary>
/// Interface for custom MongoDB ProductRepository.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetExpensiveProductsAsync(decimal minPrice);
}
