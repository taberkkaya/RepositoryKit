// IRepositoryTests.cs
using Moq;
using RepositoryKit.Core.Interfaces;

namespace RepositoryKit.Tests.UnitTests;

public class IRepositoryTests
{
    private readonly Mock<IRepository<string, Guid>> _mockRepo;

    public IRepositoryTests()
    {
        _mockRepo = new Mock<IRepository<string, Guid>>();
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Expected_Value()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockRepo.Setup(r => r.GetByIdAsync(id, default)).ReturnsAsync("value");

        // Act
        var result = await _mockRepo.Object.GetByIdAsync(id);

        // Assert
        Assert.Equal("value", result);
    }

    [Fact]
    public async Task AddAsync_Should_Invoke_Once()
    {
        var entity = "new item";
        _mockRepo.Setup(r => r.AddAsync(entity, default)).Returns(Task.CompletedTask);

        await _mockRepo.Object.AddAsync(entity);

        _mockRepo.Verify(r => r.AddAsync(entity, default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_Should_Invoke_Once()
    {
        var entity = "updated item";
        _mockRepo.Setup(r => r.UpdateAsync(entity, default)).Returns(Task.CompletedTask);

        await _mockRepo.Object.UpdateAsync(entity);

        _mockRepo.Verify(r => r.UpdateAsync(entity, default), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Invoke_Once()
    {
        var entity = "deleted item";
        _mockRepo.Setup(r => r.DeleteAsync(entity, default)).Returns(Task.CompletedTask);

        await _mockRepo.Object.DeleteAsync(entity);

        _mockRepo.Verify(r => r.DeleteAsync(entity, default), Times.Once);
    }
}