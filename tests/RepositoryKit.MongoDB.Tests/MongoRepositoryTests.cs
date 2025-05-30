using FluentAssertions;
using MongoDB.Driver;
using Moq;
using RepositoryKit.Core;

namespace RepositoryKit.MongoDB.Tests;

// Must be public for Moq to create proxies
public class TestEntity
{
    public TestEntity() { Name = string.Empty; }
    public int Id { get; set; }
    public string Name { get; set; }
}

public class MongoRepositoryTests
{
    [Fact]
    public void MongoRepository_Should_Implement_IRepository()
    {
        // Arrange - Create mock database and collection
        var mockDatabase = new Mock<IMongoDatabase>();
        var mockCollection = new Mock<IMongoCollection<TestEntity>>();

        // Setup database to return mock collection
        mockDatabase.Setup(d => d.GetCollection<TestEntity>(It.IsAny<string>(), null))
                  .Returns(mockCollection.Object);

        // Act - Create repository instance
        var repository = new MongoRepository<TestEntity, int>(mockDatabase.Object, "test");

        // Assert - Verify interface implementation
        repository.Should().BeAssignableTo<IRepository<TestEntity, int>>();
    }

    [Fact]
    public async Task MongoUnitOfWork_Should_Create_Repositories()
    {
        // Arrange - Create mock database
        var mockDatabase = new Mock<IMongoDatabase>();
        var unitOfWork = new MongoUnitOfWork(mockDatabase.Object);

        // Act - Get repository instance
        var repository = unitOfWork.GetRepository<TestEntity, int>();

        // Assert - Verify repository creation
        repository.Should().NotBeNull();
        await unitOfWork.DisposeAsync();
    }
}