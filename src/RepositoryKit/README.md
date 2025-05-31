<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit

**Modular and Testable Repository Pattern Infrastructure for .NET 9+**

</div>

---

## 🧭 Overview

**RepositoryKit** is a flexible, provider-agnostic repository pattern implementation. Designed for modern .NET 9+ projects, it encourages clean separation of concerns while supporting MongoDB, Entity Framework, and any other data provider.

---

## 🧩 Packages

| Package                                                                | Description                                      |
| ---------------------------------------------------------------------- | ------------------------------------------------ |
| [`RepositoryKit.Core`](./src/RepositoryKit.Core)                       | Interface contracts (IRepository, IQuery, IBulk) |
| [`RepositoryKit.EntityFramework`](./src/RepositoryKit.EntityFramework) | EF Core implementation                           |
| [`RepositoryKit.MongoDB`](./src/RepositoryKit.MongoDB)                 | MongoDB driver implementation                    |
| [`RepositoryKit.Extensions`](./src/RepositoryKit.Extensions)           | LINQ extensions (pagination, sorting, chunking)  |

---

## ✨ Highlights

- ✅ Plug-and-play support for multiple ORMs
- 🧪 Fully mockable interfaces for unit testing
- 🧱 Interface segregation: CRUD, Query, Bulk separated cleanly
- ⚡ Minimal dependencies, blazing fast startup

---

## 🔧 Install

Install only the pieces you need:

```bash
# For EF Core:
dotnet add package RepositoryKit.EntityFramework

# For MongoDB:
dotnet add package RepositoryKit.MongoDB

# For base contracts (automatically included):
dotnet add package RepositoryKit.Core
```

---

## 🧰 Example Usage

```csharp
// Register for EF
services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));

// Or register Mongo
services.AddScoped(typeof(IRepository<,>), typeof(MongoRepository<,>));

// Inject & use
public class ProductService
{
    private readonly IRepository<Product, Guid> _repo;

    public ProductService(IRepository<Product, Guid> repo)
    {
        _repo = repo;
    }

    public Task<Product?> Get(Guid id) => _repo.GetByIdAsync(id);
}
```

---

## 📦 NuGet

- 📦 [RepositoryKit.Core](https://www.nuget.org/packages/RepositoryKit.Core)
- 📦 [RepositoryKit.EntityFramework](https://www.nuget.org/packages/RepositoryKit.EntityFramework)
- 📦 [RepositoryKit.MongoDB](https://www.nuget.org/packages/RepositoryKit.MongoDB)
- 📦 [RepositoryKit.Extensions](https://www.nuget.org/packages/RepositoryKit.Extensions)

---

## 🧪 Testing

All interfaces can be mocked in unit tests. Full test suite is available under `tests/`.

```bash
dotnet test
```

---

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)
