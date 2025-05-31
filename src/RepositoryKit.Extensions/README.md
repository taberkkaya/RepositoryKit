<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit.Extensions

**Helper Extensions for LINQ (IQueryable & IEnumerable)**

</div>

---

## 📦 Package

This package provides utility extensions for `IQueryable` and `IEnumerable` to simplify operations like pagination, sorting, chunking, and enumeration.

---

## ✨ Features

- `Paginate()` — extension for paging `IQueryable`
- `OrderByField()` — simplified dynamic ordering
- `Chunk()` — split `IEnumerable` into subgroups
- `ForEach()` — loop over elements with action

---

## 🧰 Usage

```csharp
using RepositoryKit.Extensions;

var paged = queryableProducts.Paginate(1, 10);
var sorted = queryableProducts.OrderByField(p => p.Name);

var chunked = listOfItems.Chunk(50);
chunked.ForEach(chunk => Console.WriteLine(chunk.Count()));
```

---

## 📁 Included Extensions

### `IQueryableExtensions`

```csharp
query.Paginate(pageIndex: 2, pageSize: 10);
query.OrderByField(x => x.CreatedDate, descending: true);
```

### `IEnumerableExtensions`

```csharp
collection.Chunk(100);
collection.ForEach(x => Console.WriteLine(x));
```

---

## 📁 Requirements

- .NET 9+
- No external dependencies

---

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

> 📎 This package is standalone and does not depend on other RepositoryKit modules
