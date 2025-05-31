// samples/MongoDB.Sample/Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Sample.Models;
using RepositoryKit.MongoDB.Repositories;

namespace MongoDB.Sample.Controllers;

/// <summary>
/// Sample controller to demonstrate usage of MongoRepository.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly MongoRepository<Product, Guid> _repository;

    public ProductsController(IMongoDatabase database)
    {
        _repository = new MongoRepository<Product, Guid>(database, "Products");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(product, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
    }
}
