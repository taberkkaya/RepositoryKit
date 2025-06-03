<div align="center">
  <img src="logo-64x64.png" width="120" alt="RepositoryKit logo" />

# RepositoryKit

**Unified Repository and UnitOfWork Abstractions for Modern .NET**

</div>

---

## 🚀 What is RepositoryKit?

RepositoryKit is a **modular, provider-agnostic infrastructure library**  
that gives you clean, consistent repository and unit-of-work patterns for .NET projects.

With a single abstraction layer, you can swap between Entity Framework, MongoDB, or any custom provider  
— with no changes to your application or domain logic.

---

## 🧩 Packages

| Package                         | Purpose                                             |
| ------------------------------- | --------------------------------------------------- |
| `RepositoryKit.Core`            | Provider-agnostic interfaces and abstractions       |
| `RepositoryKit.EntityFramework` | Plug-and-play EF Core implementation                |
| `RepositoryKit.MongoDb`         | Plug-and-play MongoDB implementation                |
| `RepositoryKit.Extensions`      | Handy LINQ/collection extensions for any repository |

---

## 🛠️ Why RepositoryKit?

- **Write once, use anywhere:** Keep your app logic independent from data provider details.
- **Easily mockable & testable:** Interfaces are designed for clean dependency injection.
- **Flexible:** Add new providers or swap databases with minimal refactoring.
- **Production-ready patterns:** Follow real-world repository/unit-of-work design.
- **Extensible:** Use with your own custom repositories or add domain logic.

---

## 📦 Quick Example

### Register in your DI container (e.g., Entity Framework):

```csharp
builder.Services.AddDbContext<AppDbContext>(options => ...);
builder.Services.AddScoped<IUnitOfWork<AppDbContext>, EfUnitOfWork<AppDbContext>>();
builder.Services.AddSingleton<IUnitOfWorkManager, EfUnitOfWorkManager>();
```

## Use in your minimal API or service:

```csharp
app.MapGet("/products", async (IUnitOfWork<AppDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    return Results.Ok(products);
});
```

## 📚 Providers

- EF Core: [RepositoryKit.EntityFramework](https://github.com/taberkkaya/RepositoryKit/tree/master/src/RepositoryKit.EntityFramework)
- MongoDB: [RepositoryKit.MongoDb](https://github.com/taberkkaya/RepositoryKit/tree/master/src/RepositoryKit.MongoDB)
- Extensions: [RepositoryKit.Extensions](https://github.com/taberkkaya/RepositoryKit/tree/master/src/RepositoryKit.Extensions) for LINQ magic (Shuffle, SafeDistinct, GroupBySelect...)

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

## 🤝 Dependencies

- .NET 8+
- Minimal dependencies, designed for plug-and-play usage

---

📎 `RepositoryKit` is an open, modular infrastructure for all .NET repository scenarios.
