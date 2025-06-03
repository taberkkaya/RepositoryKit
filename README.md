<div align="center">
  <img src="assets/logo-64x64.png" width="128" alt="RepositoryKit logo" />
  <h1>RepositoryKit</h1>
  <p>
    <strong>Provider-agnostic, production-ready Repository & UnitOfWork infrastructure for modern .NET.</strong><br/>
    Clean. Modular. Mockable. <br/>
    <a href="https://github.com/taberkkaya/RepositoryKit">GitHub</a> •
    <a href="https://www.nuget.org/packages/RepositoryKit.Core">NuGet</a>
  </p>
</div>

---

## 🌍 What is RepositoryKit?

RepositoryKit is a **modular, unified infrastructure** for .NET projects  
that provides a clean and extensible Repository & UnitOfWork abstraction.

- **EF Core, MongoDB and more:** Swap your database backend with zero changes to your business logic.
- **Mockable & testable:** Designed from scratch for modern dependency injection and testing.
- **Plug & play:** Use only what you need—each provider is its own NuGet package.

---

## 📦 Packages

| Package                                                                | Description                               |
| ---------------------------------------------------------------------- | ----------------------------------------- |
| [`RepositoryKit.Core`](./src/RepositoryKit.Core)                       | Provider-agnostic abstractions/interfaces |
| [`RepositoryKit.EntityFramework`](./src/RepositoryKit.EntityFramework) | Entity Framework Core implementation      |
| [`RepositoryKit.MongoDb`](./src/RepositoryKit.MongoDb)                 | MongoDB implementation                    |
| [`RepositoryKit.Extensions`](./src/RepositoryKit.Extensions)           | Useful LINQ/collection extensions         |

---

## 🚀 Quickstart

### 1. Add a provider package:

```sh
dotnet add package RepositoryKit.EntityFramework
# or
dotnet add package RepositoryKit.MongoDb
```

### **2. Register with DI (EF example):**

```csharp
builder.Services.AddDbContext<AppDbContext>(...);
builder.Services.AddScoped<IUnitOfWork<AppDbContext>, EfUnitOfWork<AppDbContext>>();
builder.Services.AddSingleton<IUnitOfWorkManager, EfUnitOfWorkManager>();
```

### **3. Use in your app (Minimal API example):**

```csharp
app.MapGet("/products", async (IUnitOfWork<AppDbContext> uow) =>
{
    var repo = uow.GetRepository<Product>();
    var products = await repo.GetAllAsync();
    return Results.Ok(products);
});
```

### **4. Extensions for any collection/repository:**

```csharp
var distinct = products.SafeDistinct(p => p.CategoryId).ToList();
var firstOrNone = products.AsQueryable().FirstOrNone();
```

## 🧩 Why RepositoryKit?

- **Provider-agnostic:** No more vendor lock-in
- **Test-friendly:** Mock everything, everywhere
- **Production patterns:** Real-world repository & UoW
- **Minimal boilerplate:** Focus on your business logic

## 📚 Full Documentation

See individual package READMEs for full details:

- [Core](https://github.com/taberkkaya/RepositoryKit/blob/master/src/RepositoryKit.Core/README.md)
- [EntityFramework](https://github.com/taberkkaya/RepositoryKit/blob/master/src/RepositoryKit.EntityFramework/README.md)
- [MongoDb](https://github.com/taberkkaya/RepositoryKit/blob/master/src/RepositoryKit.MongoDB/README.md)
- [Extensions](https://github.com/taberkkaya/RepositoryKit/tree/master/src/RepositoryKit.Extensions)

## 🤝 Contributing

PRs, issues, and suggestions are all welcome! <br>
Feel free to fork, star, or use for your next side project.

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)
