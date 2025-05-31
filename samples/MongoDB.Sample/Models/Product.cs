// samples/MongoDB.Sample/Models/Product.cs
namespace MongoDB.Sample.Models;

/// <summary>
/// Represents a product entity in MongoDB.
/// </summary>
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
