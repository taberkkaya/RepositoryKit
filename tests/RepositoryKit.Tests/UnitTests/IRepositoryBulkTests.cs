// IRepositoryBulkTests.cs
using Moq;
using RepositoryKit.Core.Interfaces;
using Xunit;

namespace RepositoryKit.Tests.UnitTests;

public class IRepositoryBulkTests
{
    private readonly Mock<IRepositoryBulk<string>> _mockBulk;

    public IRepositoryBulkTests()
    {
        _mockBulk = new Mock<IRepositoryBulk<string>>();
    }

    [Fact]
    public async Task AddRangeAsync_Should_Invoke_Correctly()
    {
        // Arrange
        var data = new[] { "A", "B" };
        _mockBulk.Setup(b => b.AddRangeAsync(data, default)).Returns(Task.CompletedTask);

        // Act
        await _mockBulk.Object.AddRangeAsync(data);

        // Assert
        _mockBulk.Verify(b => b.AddRangeAsync(data, default), Times.Once);
    }

    [Fact]
    public async Task UpdateRangeAsync_Should_Invoke_Correctly()
    {
        // Arrange
        var data = new[] { "X", "Y" };
        _mockBulk.Setup(b => b.UpdateRangeAsync(data, default)).Returns(Task.CompletedTask);

        // Act
        await _mockBulk.Object.UpdateRangeAsync(data);

        // Assert
        _mockBulk.Verify(b => b.UpdateRangeAsync(data, default), Times.Once);
    }

    [Fact]
    public async Task DeleteRangeAsync_Should_Invoke_Correctly()
    {
        // Arrange
        var data = new[] { "X", "Y" };
        _mockBulk.Setup(b => b.DeleteRangeAsync(data, default)).Returns(Task.CompletedTask);

        // Act
        await _mockBulk.Object.DeleteRangeAsync(data);

        // Assert
        _mockBulk.Verify(b => b.DeleteRangeAsync(data, default), Times.Once);
    }
}
