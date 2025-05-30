using FluentAssertions;
using Moq;

namespace RepositoryKit.Core.Tests;

public class RepositoryTests
{
    // Test entity class
    public class TestEntity
    {
        public TestEntity() { Name = string.Empty; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Fact]
    public void IRepository_Should_Define_Basic_CRUD_Operations()
    {
        // Arrange - Create mock repository
        var mockRepo = new Mock<IRepository<TestEntity, int>>();

        // First add interface casting before accessing Object property
        var readOnlyRepo = mockRepo.As<IReadOnlyRepository<TestEntity, int>>();

        // Setup mock methods
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync((int id) => new TestEntity { Id = id });

        mockRepo.Setup(r => r.AddAsync(It.IsAny<TestEntity>()))
               .Returns(Task.CompletedTask);

        // Assert - Verify interface implementations
        mockRepo.Object.Should().NotBeNull();
        readOnlyRepo.Object.Should().NotBeNull();
    }

    [Fact]
    public async Task Repository_Should_Implement_Basic_CRUD()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<TestEntity, int>>();
        var testEntity = new TestEntity { Id = 1, Name = "Test" };

        // Setup mock returns
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(testEntity);
        mockRepo.Setup(r => r.AddAsync(It.IsAny<TestEntity>())).Returns(Task.CompletedTask);

        // Act - Test the operations
        await mockRepo.Object.AddAsync(testEntity);
        var result = await mockRepo.Object.GetByIdAsync(1);

        // Assert - Verify results
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Test");
    }
}