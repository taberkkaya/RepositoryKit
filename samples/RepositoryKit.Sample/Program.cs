using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core;
using RepositoryKit.EntityFramework;
using RepositoryKit.Sample.Data;
using RepositoryKit.Sample.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)));

// RepositoryKit'i ekle
builder.Services.AddRepositoryKitWithEntityFramework<AppDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    try
    {
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var repo = unitOfWork.GetRepository<Product, int>();

        // Check if any products exist (no predicate needed)
        if (!(await repo.GetAllAsync()).Any())
        {
            await repo.AddRangeAsync([
                new Product { Name = "Laptop", Price = 1500, Stock = 10 },
                new Product { Name = "Mouse", Price = 25, Stock = 50 },
                new Product { Name = "Keyboard", Price = 45, Stock = 30 }
                ]
            );
            await unitOfWork.CommitAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database");
    }
}

app.Run();