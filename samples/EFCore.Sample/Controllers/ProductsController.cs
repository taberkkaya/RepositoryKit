// samples/EFCore.Sample/Controllers/ProductsController.cs
using EFCore.Sample.Data;
using Microsoft.AspNetCore.Mvc;
using RepositoryKit.EntityFramework.Repositories;

namespace EFCore.Sample.Controllers;

/// <summary>
/// Sample controller to demonstrate usage of EFRepository.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly EFRepository<Product, Guid> _repository;

    public ProductsController(SampleDbContext context)
    {
        _repository = new EFRepository<Product, Guid>(context);
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
