using MongoDB.Driver;
using MongoDB.Sample.Models;
using RepositoryKit.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDb and repository registration
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient("mongodb://localhost:27017"));
builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("SampleDb"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

/// <summary>
/// Returns all products.
/// </summary>
app.MapGet("/products", async (IProductRepository repo) =>
{
    var products = await repo.GetAllAsync();
    return Results.Ok(products);
})
.WithName("GetProducts").WithOpenApi();

/// <summary>
/// Returns products with price greater than the specified minimum.
/// </summary>
app.MapGet("/products/expensive", async (IProductRepository repo, decimal minPrice) =>
{
    var expensive = await repo.GetExpensiveProductsAsync(minPrice);
    return Results.Ok(expensive);
})
.WithName("ExpensiveProducts").WithOpenApi();

/// <summary>
/// Returns the first product using FirstOrNone extension.
/// </summary>
app.MapGet("/products/first", async (IProductRepository repo) =>
{
    var products = await repo.GetAllAsync();
    var first = products.AsQueryable().FirstOrNone();
    return Results.Ok(first);
})
.WithName("FirstProduct").WithOpenApi();

/// <summary>
/// Returns all products in random order using Shuffle extension.
/// </summary>
app.MapGet("/products/shuffle", async (IProductRepository repo) =>
{
    var products = await repo.GetAllAsync();
    return Results.Ok(products.Shuffle().ToList());
})
.WithName("ShuffleProducts").WithOpenApi();

/// <summary>
/// Returns distinct products by price using SafeDistinct extension.
/// </summary>
app.MapGet("/products/distinct", async (IProductRepository repo) =>
{
    var products = await repo.GetAllAsync();
    var distinct = products.SafeDistinct(p => p.Price).ToList();
    return Results.Ok(distinct);
})
.WithName("DistinctProducts").WithOpenApi();

/// <summary>
/// Applies an uppercase transformation to all product names using ForEach extension.
/// </summary>
app.MapGet("/products/uppercase", async (IProductRepository repo) =>
{
    var products = await repo.GetAllAsync();
    products.ForEach(p => p.Name = p.Name.ToUpperInvariant());
    return Results.Ok(products);
})
.WithName("UppercaseProducts").WithOpenApi();

/// <summary>
/// Groups products by price and projects using GroupBySelect extension.
/// </summary>
app.MapGet("/products/groupby", async (IProductRepository repo) =>
{
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
app.MapPost("/products", async (IProductRepository repo, Product product) =>
{
    await repo.AddAsync(product);
    return Results.Created($"/products/{product.Id}", product);
})
.WithName("CreateProduct").WithOpenApi();

/// <summary>
/// Updates an existing product.
/// </summary>
app.MapPut("/products/{id}", async (IProductRepository repo, Guid id, Product input) =>
{
    var product = await repo.GetAsync(p => p.Id == id);
    if (product is null) return Results.NotFound();

    product.Name = input.Name;
    product.Price = input.Price;
    await repo.UpdateAsync(product);
    return Results.NoContent();
})
.WithName("UpdateProduct").WithOpenApi();

/// <summary>
/// Deletes a product by id.
/// </summary>
app.MapDelete("/products/{id}", async (IProductRepository repo, Guid id) =>
{
    var product = await repo.GetAsync(p => p.Id == id);
    if (product is null) return Results.NotFound();

    await repo.DeleteAsync(product);
    return Results.NoContent();
})
.WithName("DeleteProduct").WithOpenApi();

app.Run();
