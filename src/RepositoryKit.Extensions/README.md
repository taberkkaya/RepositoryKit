<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit.Extensions

**Reusable LINQ and Collection Extensions for RepositoryKit and .NET**

</div>

---

## 📦 Package

This package provides **provider-agnostic extension methods** for LINQ queries and collections.

- Designed for use with any data provider (EF, Mongo, Dapper, InMemory, etc.)
- No dependencies except for .NET Standard LINQ
- All methods are static, lightweight, and high performance

---

## ✅ Extensions

| File                       | Highlights & Example Methods                              |
| -------------------------- | --------------------------------------------------------- |
| `IQueryableExtensions.cs`  | `ToPagedList`, `ApplySorting`, `DynamicWhere`, `SelectAs` |
| `IEnumerableExtensions.cs` | `ForEach`, `SafeDistinct`, `Shuffle`, `GroupBySelect`     |

---

## 📁 Typical Usage

Chain extension methods in your query and projection flow:

```csharp
var paged = db.Products.Query().ApplySorting("Name").ToPagedList(page: 1, pageSize: 10);

var summaries = paged.SelectAs(x => new ProductSummaryDto
{
    Id = x.Id,
    Name = x.Name
});

paged.ForEach(product => Console.WriteLine(product.Name));
```

## ✨ Features

- Pure extension methods for `IQueryable<T>` and `IEnumerable<T>`
- Plug-and-play usage: no extra setup required
- All methods work on any provider or in-memory source

## 🤝 Dependencies

- No external dependencies (just .NET Standard LINQ)

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

📎 Designed to be used with `RepositoryKit`, but useful in any .NET project
