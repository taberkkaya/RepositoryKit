// IRepositoryQueryTests.cs 
using Moq;
using RepositoryKit.Core.Interfaces;
using Xunit;
using System.Linq.Expressions;

namespace RepositoryKit.Tests.UnitTests;

public class IRepositoryQueryTests
{
    private readonly Mock<IRepositoryQuery<string, Guid>> _mockQuery;

    public IRepositoryQueryTests()
    {
        _mockQuery = new Mock<IRepositoryQuery<string, Guid>>();
    }

    [Fact]
    public async Task FindAsync_Should_Call_With_Correct_Predicate()
    {
        // Arrange
        Expression<Func<string, bool>> predicate = x => x.Contains("test");
        _mockQuery.Setup(q => q.FindAsync(predicate, default)).ReturnsAsync(new[] { "test1", "test2" });

        // Act
        var result = await _mockQuery.Object.FindAsync(predicate);

        // Assert
        Assert.Contains("test1", result);
        _mockQuery.Verify(q => q.FindAsync(predicate, default), Times.Once);
    }

    [Fact]
    public async Task GetPagedAsync_Should_Return_Correct_Data()
    {
        // Arrange
        var expected = new[] { "one", "two" };
        _mockQuery.Setup(r => r.GetPagedAsync(1, 2, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockQuery.Object.GetPagedAsync(1, 2);

        // Assert
        Assert.Equal(2, result.Count());
        _mockQuery.Verify(r => r.GetPagedAsync(1, 2, default), Times.Once);
    }

    [Fact]
    public void AsQueryable_Should_Be_Callable()
    {
        // Arrange
        var data = new[] { "x", "y" }.AsQueryable();
        _mockQuery.Setup(r => r.AsQueryable()).Returns(data);

        // Act
        var result = _mockQuery.Object.AsQueryable();

        // Assert
        Assert.Equal(2, result.Count());
    }
}
