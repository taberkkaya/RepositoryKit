namespace EFCore.Sample.Data;

using EFCore.Sample.Entities;
using Microsoft.EntityFrameworkCore;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
