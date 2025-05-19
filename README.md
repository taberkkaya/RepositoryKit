<p align="center">
  <img src="icon.jpg" alt="RepositoryKit Logo" width="200"/>
</p>

<h1 align="center">RepositoryKit</h1>

<p align="center">
  🧰 Lightweight, generic repository abstraction for Entity Framework Core.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet" />
  <img src="https://img.shields.io/badge/EF--Core-Compatible-success?logo=entity-framework" />
  <img src="https://img.shields.io/badge/License-MIT-informational" />
</p>

---

## 📦 What is RepositoryKit?

> **RepositoryKit** is a lightweight, extensible, and clean implementation of the Repository and Unit of Work pattern for **Entity Framework Core**. It's designed to eliminate repetitive data-access boilerplate and promote separation of concerns — without the bloat of full-scale ORMs or frameworks.

## 🚀 Features

✔️ Asynchronous CRUD operations  
✔️ Tracking and no-tracking query support  
✔️ Minimal dependencies — no external clutter  
✔️ Extensible base interfaces for full control  
✔️ Ideal for clean architecture setups  
✔️ MIT Licensed — Free for personal & commercial use

## 🧰 Technologies Used

| Technology                                                                | Purpose                                 |
| ------------------------------------------------------------------------- | --------------------------------------- |
| [.NET 9](https://dotnet.microsoft.com/)                                   | Core runtime                            |
| [Entity Framework Core](https://docs.microsoft.com/ef/core/)              | ORM support                             |
| [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html) | Decouples domain logic from data access |
| [Unit of Work](https://martinfowler.com/eaaCatalog/unitOfWork.html)       | Efficient transaction management        |

---

## 📦 Installation

Using NuGet Package Manager Console:

```bash
Install-Package RepositoryKit
```

Or via .NET CLI:

```
dotnet add package RepositoryKit
```

## 📂 Basic Usage

> 1.  First, create your own domain-specific repository interface that inherits from the generic `IRepository<T>` provided by RepositoryKit:

```csharp
using RepositoryKit

public interface IProductRepository : IRepository<Product>
{
    // Add any product-specific methods here
}
```

> 2.  Then, implement this interface with a concrete repository class, inheriting from the generic `Repository<T, TContext>`:

```csharp
using RepositoryKit

public class ProductRepository : Repository<Product, AppDbContext>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    // Implement any product-specific repository methods here
}

```

> 3.  Register your repository in the Dependency Injection container:

```csharp
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());
```

> 4.  Now you can inject and use IProductRepository in your services or controllers:

```csharp
public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }
}
```

---

### ✨ Contribution

> Feel free to fork this repository and contribute your improvements.

---

### 🪪 License

> This project is open-source and available under the MIT License.

---

### 🧠 Inspired By

> This project is inspired by the work of [Taner Saydam](https://github.com/TanerSaydam).  
> Check out his GitHub profile and repositories for more: https://github.com/TanerSaydam

---

<p align="center"> <img src="https://skillicons.dev/icons?i=dotnet,github,visualstudio" /> </p>
