<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit.MongoDB

**MongoDB Driver Implementation for RepositoryKit**

</div>

---

## 📦 Package

Provides a repository implementation using **MongoDB.Driver**. It supports flexible querying and follows the same interface contract as EF version.

---

## ✨ Features

- `MongoRepository<T, TKey>` implements all core contracts
- Async-first design
- Supports filtering, sorting, and paging
- Clean integration with MongoDB collections

---

## 🧰 Usage

```csharp
services.Configure<MongoDbOptions>(Configuration.GetSection("MongoDb"));
services.AddSingleton<IMongoClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<MongoDbOptions>>().Value;
    return new MongoClient(options.ConnectionString);
});
services.AddScoped<IMongoDatabase>(sp =>
{
    var options = sp.GetRequiredService<IOptions<MongoDbOptions>>().Value;
    return sp.GetRequiredService<IMongoClient>().GetDatabase(options.Database);
});

services.AddScoped(typeof(IRepository<,>), typeof(MongoRepository<,>));
```

---

## 🔍 Example

```csharp
// appsettings.json
"MongoDb": {
  "ConnectionString": "mongodb://localhost:27017",
  "Database": "RepositoryKitDb"
}

// Define your entity
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

// Injected service
private readonly IRepository<Product, Guid> _repo;

public ProductService(IRepository<Product, Guid> repo)
{
    _repo = repo;
}

// Usage
await _repo.AddAsync(new Product { Id = Guid.NewGuid(), Name = "Pen" });
```

---

## 📁 Requirements

- MongoDB.Driver
- Microsoft.Extensions.Options

---

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

> 📎 This package depends on `RepositoryKit.Core`
