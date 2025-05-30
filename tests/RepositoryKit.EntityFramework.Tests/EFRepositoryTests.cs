using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace RepositoryKit.EntityFramework.Tests;

public class EFRepositoryTests
{
    // Test DbContext implementation
    private class TestDbContext : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options) { }
    }

    // Test entity class
    public class TestEntity
    {
        public TestEntity() { Name = string.Empty; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Fact]
    public async Task EFRepository_Should_Work_With_InMemory_Database()
    {
        // Arrange - Configure in-memory database
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using (var context = new TestDbContext(options))
        {
            var repository = new EFRepository<TestEntity, int>(context);

            // Act - Test repository operations
            await repository.AddAsync(new TestEntity { Id = 1, Name = "Test" });
            await context.SaveChangesAsync();

            // Assert - Verify results
            var result = await repository.GetByIdAsync(1);
            result.Should().NotBeNull();
            result.Name.Should().Be("Test");
        }
    }
}