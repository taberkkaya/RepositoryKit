<div align="center">
  <img src="assets/logo-1250x1250.png" width="120" alt="RepositoryKit logo" />
  
  # RepositoryKit
  **A Modular & Extendable Repository Pattern Implementation for .NET**
</div>

---

## ✨ Overview

**RepositoryKit** is a lightweight and extensible repository abstraction library for .NET projects. It encapsulates common data access operations and allows seamless integration with multiple data providers like Entity Framework and MongoDB.

---

## 📦 Packages

| Package                         | Description                               |
| ------------------------------- | ----------------------------------------- |
| `RepositoryKit.Core`            | Interfaces and base contracts             |
| `RepositoryKit.EntityFramework` | EF Core implementation                    |
| `RepositoryKit.MongoDB`         | MongoDB driver implementation             |
| `RepositoryKit.Extensions`      | Helper extensions for IQueryable and more |

---

## 🛠️ Installation

You can install each package separately:

```bash
# Core interfaces
Install-Package RepositoryKit.Core

# EF implementation
Install-Package RepositoryKit.EntityFramework

# MongoDB implementation
Install-Package RepositoryKit.MongoDB
```

---

## 🚀 Getting Started

```csharp
// Register the desired implementation
services.AddScoped<IRepository<Product, Guid>, EFRepository<Product, Guid>>();
```

---

## 🧪 Unit Testing

Mockable interfaces make testing your repository-based logic clean and simple:

```csharp
var mock = new Mock<IRepository<Product, Guid>>();
mock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);
```

To run the unit tests:

```bash
dotnet test tests/RepositoryKit.Tests
```

---

## 📁 Folder Structure

```
src/
├── RepositoryKit                # Umbrella entry (optional)
├── RepositoryKit.Core           # Interface contracts
├── RepositoryKit.EntityFramework
├── RepositoryKit.MongoDB
├── RepositoryKit.Extensions
samples/
├── EF.Sample                    # EFCore demo
├── MongoDB.Sample               # Mongo demo
tests/
└── RepositoryKit.Tests          # Unit tests
```

---

## 🧱 Architecture

- `IRepository<T, TKey>` — CRUD operations
- `IRepositoryQuery<T, TKey>` — advanced query (paging, sorting)
- `IRepositoryBulk<T>` — batch operations

All implementations follow **SOLID principles** and are unit-test friendly.

---

## 💡 Motivation

Writing repository logic repeatedly? RepositoryKit offers:

- 🔁 Reusability across projects
- 🧪 Easy testability
- 🧩 Plug-n-play data provider support
- 🧼 Clean and minimal API

---

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

> 📎 For detailed package-specific README files, see each subfolder under `/src`

---

<div align="center">
Made with ❤️ by [@taberkkaya](https://github.com/taberkkaya)
</div>
