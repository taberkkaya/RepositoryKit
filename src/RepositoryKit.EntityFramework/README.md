<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit.EntityFramework

**Entity Framework Core Implementation for RepositoryKit**

</div>

---

## 📦 Package

This package contains the **Entity Framework Core** based implementation of the RepositoryKit abstractions.

---

## ✅ Implementations

| Class                                    | Purpose                                    |
| ---------------------------------------- | ------------------------------------------ |
| `EfReadOnlyRepository<TEntity,TContext>` | Read-only LINQ queries on your entities    |
| `EfRepository<TEntity, TContext>`        | Full-featured CRUD repository for EF Core  |
| `EfUnitOfWork<TContext>`                 | Unit of Work for a single DbContext        |
| `EfUnitOfWorkManager`                    | Multi-context Unit of Work resolver via DI |

---

## 🚀 Usage Examples

### **1. Register in DI (Startup or Program.cs)**

```csharp
// Register DbContext and RepositoryKit services
builder.Services.AddDbContext<SampleDbContext>(opt => opt.UseInMemoryDatabase("SampleDb"));
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(EfUnitOfWork<>));
builder.Services.AddSingleton<IUnitOfWorkManager, EfUnitOfWorkManager>();

// Custom repository registration (interface-based)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
```

### **2. Basic Repository & UnitOfWork Usage (Minimal API)**

```csharp
app.MapPost("/products", async (IUnitOfWork<SampleDbContext> uow, Product product) =>
{
    var repo = uow.GetRepository<Product>();
    await repo.AddAsync(product);
    await uow.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});
```

### **3. Custom Repository Inheritance & Usage**

Custom Repository Interface and Implementation

```csharp
public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetExpensiveProductsAsync(decimal minPrice);
}

public class ProductRepository : EfRepository<Product, SampleDbContext>, IProductRepository
{
    public ProductRepository(SampleDbContext context) : base(context) { }

    public async Task<List<Product>> GetExpensiveProductsAsync(decimal minPrice)
    {
        return await _dbSet.Where(p => p.Price > minPrice).ToListAsync();
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

### **4. Multiple Contexts with IUnitOfWorkManager**

```csharp
app.MapGet("/multi-context-demo", (IUnitOfWorkManager uowManager) =>
{
    var uow = uowManager.GetUnitOfWork<SampleDbContext>();
    var repo = uow.GetRepository<Product>();
    // Use repo as needed...
});
```

## 🚨 Exception Handling

All repository and unit of work operations wrap provider or database exceptions with the standard `RepositoryException`:`

```csharp
try
{
    await repository.AddAsync(product);
    await unitOfWork.SaveChangesAsync();
}
catch (RepositoryException ex) when (ex.ErrorType == RepositoryErrorType.Add)
{
    // Handle or log rich error info
}
```

## 🤝 Dependencies

- [RepositoryKit.Core](https://github.com/taberkkaya/RepositoryKit/tree/master/src/RepositoryKit.Core)
- Microsoft.EntityFrameworkCore (as implementation dependency)

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

📎 This package is the official EF Core provider for `RepositoryKit`
