// File: EFCore.Sample/Program.cs

using EFCore.Sample.Data;
using EFCore.Sample.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core.Interfaces;
using RepositoryKit.EntityFramework.Implementations;
using RepositoryKit.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SampleDbContext>(opt => opt.UseInMemoryDatabase("SampleDb"));

builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(EfUnitOfWork<>));
builder.Services.AddSingleton<IUnitOfWorkManager, EfUnitOfWorkManager>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SampleDbContext>();
    db.Products.AddRange(
        new Product { Id = Guid.NewGuid(), Name = "Keyboard", Price = 299 },
        new Product { Id = Guid.NewGuid(), Name = "Monitor", Price = 1799 },
        new Product { Id = Guid.NewGuid(), Name = "Mouse", Price = 199 },
        new Product { Id = Guid.NewGuid(), Name = "Speaker", Price = 499 }
    );
    db.SaveChanges();
}

/// <summary>
/// Returns a paged and sorted list of products.
/// Uses ApplySorting and ToPagedList extensions.
/// </summary>
app.MapGet("/products", async (IUnitOfWork<SampleDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    var result = products.AsQueryable()
        .ApplySorting("Name")
        .ToPagedList(1, 10);
    return Results.Ok(result);
})
.WithName("GetProducts").WithOpenApi();

/// <summary>
/// Returns products filtered by name using DynamicWhere extension.
/// </summary>
app.MapGet("/products/search", async (IUnitOfWork<SampleDbContext> uow, string? name) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    var query = products.AsQueryable()
        .DynamicWhere(string.IsNullOrWhiteSpace(name) ? null : p => p.Name.Contains(name!));
    return Results.Ok(query.ToList());
})
.WithName("SearchProducts").WithOpenApi();

/// <summary>
/// Returns a projection (summary) of products using SelectAs extension.
/// </summary>
app.MapGet("/products/summaries", async (IUnitOfWork<SampleDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    var summaries = products.AsQueryable()
        .SelectAs(p => new { p.Id, p.Name, PriceWithTax = p.Price * 1.2m })
        .ToList();
    return Results.Ok(summaries);
})
.WithName("ProductSummaries").WithOpenApi();

/// <summary>
/// Returns the first product using FirstOrNone extension.
/// </summary>
app.MapGet("/products/first", async (IUnitOfWork<SampleDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    var first = products.AsQueryable().FirstOrNone();
    return Results.Ok(first);
})
.WithName("FirstProduct").WithOpenApi();

/// <summary>
/// Returns all products in random order using Shuffle extension.
/// </summary>
app.MapGet("/products/shuffle", async (IUnitOfWork<SampleDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    return Results.Ok(products.Shuffle().ToList());
})
.WithName("ShuffleProducts").WithOpenApi();

/// <summary>
/// Returns distinct products by price using SafeDistinct extension.
/// </summary>
app.MapGet("/products/distinct", async (IUnitOfWork<SampleDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    var distinct = products.SafeDistinct(p => p.Price).ToList();
    return Results.Ok(distinct);
})
.WithName("DistinctProducts").WithOpenApi();

/// <summary>
/// Applies an uppercase transformation to all product names using ForEach extension.
/// </summary>
app.MapGet("/products/uppercase", async (IUnitOfWork<SampleDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    products.ForEach(p => p.Name = p.Name.ToUpperInvariant());
    return Results.Ok(products);
})
.WithName("UppercaseProducts").WithOpenApi();

/// <summary>
/// Groups products by price and projects using GroupBySelect extension.
/// </summary>
app.MapGet("/products/groupby", async (IUnitOfWork<SampleDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    var groups = products.GroupBySelect(
        p => p.Price,
        (price, group) => new { Price = price, Count = group.Count(), Products = group.ToList() }
    ).ToList();
    return Results.Ok(groups);
})
.WithName("GroupByProducts").WithOpenApi();

/// <summary>
/// Creates a new product.
/// </summary>
app.MapPost("/products", async (IUnitOfWork<SampleDbContext> uow, Product product) =>
{
    var repo = uow.GetRepository<Product>();
    await repo.AddAsync(product);
    await uow.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
})
.WithName("CreateProduct").WithOpenApi();

/// <summary>
/// Updates an existing product.
/// </summary>
app.MapPut("/products/{id}", async (IUnitOfWork<SampleDbContext> uow, Guid id, Product input) =>
{
    var repo = uow.GetRepository<Product>();
    var product = await repo.GetAsync(p => p.Id == id);
    if (product is null) return Results.NotFound();

    product.Name = input.Name;
    product.Price = input.Price;
    await repo.UpdateAsync(product);
    await uow.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("UpdateProduct").WithOpenApi();

/// <summary>
/// Deletes a product by id.
/// </summary>
app.MapDelete("/products/{id}", async (IUnitOfWork<SampleDbContext> uow, Guid id) =>
{
    var repo = uow.GetRepository<Product>();
    var product = await repo.GetAsync(p => p.Id == id);
    if (product is null) return Results.NotFound();

    await repo.DeleteAsync(product);
    await uow.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteProduct").WithOpenApi();

/// <summary>
/// Demonstrates using IUnitOfWorkManager to resolve repositories from different DbContexts.
/// </summary>
app.MapGet("/multi-context-demo", (IUnitOfWorkManager uowManager) =>
{
    var uow = uowManager.GetUnitOfWork<SampleDbContext>();
    var repo = uow.GetRepository<Product>();
    return Results.Ok($"Repository resolved from UnitOfWorkManager: {repo.GetType().Name}");
})
.WithName("MultiContextDemo").WithOpenApi();

/// <summary>
/// Demonstrates manual UnitOfWork scope and disposal.
/// </summary>
app.MapGet("/uow-scope", async (IServiceProvider sp) =>
{
    await using var scope = sp.CreateAsyncScope();
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork<SampleDbContext>>();
    var repo = uow.GetRepository<Product>();
    var count = (await repo.GetAllAsync()).Count();
    return Results.Ok(new { ProductCount = count });
})
.WithName("UnitOfWorkScopeDemo").WithOpenApi();

/// <summary>
/// Returns products with price greater than the specified minimum.
/// </summary>
app.MapGet("/products/expensive", async (IProductRepository repo, decimal minPrice) =>
{
    var expensive = await repo.GetExpensiveProductsAsync(minPrice);
    return Results.Ok(expensive);
})
.WithName("ExpensiveProducts").WithOpenApi();

app.Run();