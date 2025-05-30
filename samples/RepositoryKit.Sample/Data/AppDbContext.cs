using Microsoft.EntityFrameworkCore;
using RepositoryKit.Sample.Models;

namespace RepositoryKit.Sample.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}