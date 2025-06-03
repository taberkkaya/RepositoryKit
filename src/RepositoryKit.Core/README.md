<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit.Core

**Abstractions and Contracts for RepositoryKit**

</div>

---

## 📦 Package

This package defines the **interfaces**, **contracts**, and **domain exceptions** that all `RepositoryKit` providers depend on.

It contains no external dependencies.

---

## ✅ Interfaces

| Interface                | Purpose                                          |
| ------------------------ | ------------------------------------------------ |
| `IReadOnlyRepository<T>` | Abstraction for read-only queries                |
| `IRepository<T>`         | Generic CRUD operations for an entity            |
| `IUnitOfWork<TContext>`  | Transaction boundary for one context (DbContext) |
| `IUnitOfWorkManager`     | Multi-context Unit of Work provider              |

---

## 🚨 Exception

| Exception             | Purpose                                        |
| --------------------- | ---------------------------------------------- |
| `RepositoryException` | Standard error with context (type, op, entity) |

- Provides rich error information with type, entity, operation, and inner exception.
- Used by all RepositoryKit providers for consistent exception handling.

---

## 📁 Typical Usage

You don't use `RepositoryKit.Core` directly, but instead **reference it** from your repository implementation or consume its abstractions:

```csharp
public class EfRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    // Implementation using RepositoryKit.Core interfaces
}

// Exception usage in a provider implementation:
throw new RepositoryException(
    "A database error occurred while adding an entity.",
    RepositoryErrorType.Add,
    typeof(Product),
    nameof(AddAsync),
    ex
);
```

## 🧪 Testing with Mocks

```csharp
var mock = new Mock<IRepository<Product>>();
mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(productsList);
```

## 🤝 Dependencies

- Microsoft.EntityFrameworkCore (for `DbContext`-based contracts)
- No other dependencies
- Designed to be stable, lightweight, and fully mockable

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

📎 This package is automatically referenced by all `RepositoryKit.*` implementations
