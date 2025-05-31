// samples/EFCore.Sample/Data/SampleDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace EFCore.Sample.Data;

/// <summary>
/// Sample DbContext used to demonstrate EF integration.
/// </summary>
public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
    }
}

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
