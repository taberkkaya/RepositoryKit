<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit.Core

**Abstractions and Contracts for RepositoryKit**

</div>

---

## 📦 Package

This package defines the **interfaces**, **contracts**, and **common abstractions** that every `RepositoryKit` provider depends on.

It contains no external dependencies.

---

## ✅ Interfaces

| Interface                   | Purpose                                     |
| --------------------------- | ------------------------------------------- |
| `IRepository<T, TKey>`      | Basic CRUD operations                       |
| `IRepositoryQuery<T, TKey>` | Query: `Find`, `GetById`, `Paged`, `Sorted` |
| `IRepositoryBulk<T>`        | Bulk operations for add/update/delete       |

---

## 📁 Typical Usage

You don't use `RepositoryKit.Core` directly, but instead **reference it** from your repository implementation:

```csharp
public class EFRepository<T, TKey> : IRepository<T, TKey>, IRepositoryQuery<T, TKey>, IRepositoryBulk<T>
{
    // ...
}
```

This way, you can swap out your data source while keeping your application code untouched.

---

## 🧪 Testing with Mocks

```csharp
var mock = new Mock<IRepository<Product, Guid>>();
mock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);
```

---

## 🤝 Dependencies

- No dependencies
- Designed to be stable, lightweight, and fully mockable

---

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

> 📎 This package is automatically referenced by all `RepositoryKit.*` implementations
