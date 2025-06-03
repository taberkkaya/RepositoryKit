using MongoDB.Driver;
using RepositoryKit.MongoDb.Implementations;

namespace MongoDB.Sample.Models;

/// <summary>
/// Custom MongoDB repository for Product entity.
/// </summary>
public class ProductRepository : MongoRepository<Product, IMongoDatabase>, IProductRepository
{
    public ProductRepository(IMongoDatabase db) : base(db) { }

    public async Task<List<Product>> GetExpensiveProductsAsync(decimal minPrice)
    {
        return await _collection.Find(p => p.Price > minPrice).ToListAsync();
    }
}
