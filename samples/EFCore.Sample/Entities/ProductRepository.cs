// File: EFCore.Sample/Entities/ProductRepository.cs

using RepositoryKit.EntityFramework.Implementations;
using EFCore.Sample.Data;
using EFCore.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Sample.Entities;

/// <summary>
/// Custom repository for Product entity with additional business methods.
/// </summary>
public class ProductRepository : EfRepository<Product, SampleDbContext>, IProductRepository
{
    public ProductRepository(SampleDbContext context) : base(context) { }

    /// <summary>
    /// Gets products with price above the given threshold.
    /// </summary>
    public async Task<List<Product>> GetExpensiveProductsAsync(decimal minPrice)
    {
        return await _dbSet.Where(p => p.Price > minPrice).ToListAsync();
    }
}
