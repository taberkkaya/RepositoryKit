<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit.MongoDb

**MongoDB Implementation for RepositoryKit**

</div>

---

## 📦 Package

This package provides the **MongoDB** implementation of RepositoryKit’s abstractions.

It enables consistent, testable, and flexible repository patterns for MongoDB projects —  
without unnecessary UnitOfWork or transaction layers.

---

## ✅ Implementations

| Class                                        | Purpose                                   |
| -------------------------------------------- | ----------------------------------------- |
| `MongoReadOnlyRepository<TEntity, TContext>` | Read-only LINQ-style queries for MongoDB  |
| `MongoRepository<TEntity, TContext>`         | Full-featured CRUD repository for MongoDB |

---

## 🚀 Usage Examples

### **1. Register in DI (Startup or Program.cs)**

```csharp
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient("mongodb://localhost:27017"));
builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("SampleDb"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
```

### **2. Basic Repository Usage (Minimal API)**

```csharp
app.MapPost("/products", async (IProductRepository repo, Product product) =>
{
    await repo.AddAsync(product);
    return Results.Created($"/products/{product.Id}", product);
});
```

### **3. Custom Repository Inheritance & Usage**

#### Custom Repository Interface and Implementation

```csharp
public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetExpensiveProductsAsync(decimal minPrice);
}

public class ProductRepository : MongoRepository<Product, IMongoDatabase>, IProductRepository
{
    public ProductRepository(IMongoDatabase db) : base(db) { }

    public async Task<List<Product>> GetExpensiveProductsAsync(decimal minPrice)
    {
        return await _collection.Find(p => p.Price > minPrice).ToListAsync();
    }
}
```

#### Endpoint Usage

```csharp
app.MapGet("/products/expensive", async (IProductRepository repo, decimal minPrice) =>
{
    var expensive = await repo.GetExpensiveProductsAsync(minPrice);
    return Results.Ok(expensive);
});
```

### **4. All RepositoryKit.Extensions Can Be Used**

All LINQ-based [RepositoryKit.Extensions](https://github.com/taberkkaya/RepositoryKit/tree/master/src/RepositoryKit.Extensions) (like `Shuffle`, `FirstOrNone`, `GroupBySelect`, `SafeDistinct`, etc.)
can be used directly in your MongoDB sample:

```csharp
var products = await repo.GetAllAsync();
var shuffled = products.Shuffle().ToList();
```

## 🚨 Exception Handling

Repository operations wrap errors with the standard `RepositoryException` from `RepositoryKit.Core`:

```csharp
try
{
    await repo.AddAsync(product);
}
catch (RepositoryException ex)
{
    // Handle or log error info
}
```

## 🤝 Dependencies

- [RepositoryKit.Core](https://github.com/taberkkaya/RepositoryKit/tree/master/src/RepositoryKit.Core)
- MongoDB.Driver (official MongoDB driver)

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

📎 This package is the official MongoDB provider for `RepositoryKit`.
