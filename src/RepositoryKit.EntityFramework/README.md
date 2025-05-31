<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit.EntityFramework

**Entity Framework Core Implementation for RepositoryKit**

</div>

---

## 📦 Package

Provides an out-of-the-box repository implementation using **Entity Framework Core**. Supports `DbContext`, `DbSet<T>` and leverages EF Core’s change tracking.

---

## ✨ Features

- `EFRepository<T, TKey>` implements all core contracts
- `AsNoTracking` support for queries
- Async support for all operations
- Supports pagination, sorting, and filtered queries

---

## 🧰 Usage

```csharp
services.AddDbContext<AppDbContext>();

// Register EFRepository with open generics (recommended)
services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));

// Optional: You can still register specific types manually if needed
// services.AddScoped<IRepository<Product, Guid>>(provider =>
//     new EFRepository<Product, Guid>(provider.GetRequiredService<AppDbContext>()));
```

---

## 🔍 Example

```csharp
// Define your entity
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Inject the repository (e.g. in controller or service)
private readonly IRepository<Product, Guid> _repo;

public ProductService(IRepository<Product, Guid> repo)
{
    _repo = repo;
}

// Use it
var product = await _repo.GetByIdAsync(id);
var expensive = await _repo.FindAsync(p => p.Price > 100, tracking: false);
```

---

## 📁 Requirements

- Microsoft.EntityFrameworkCore
- Compatible with .NET 9+

---

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

> 📎 This package depends on `RepositoryKit.Core
