using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MongoDB.Sample.Models;

/// <summary>
/// Sample Product entity for MongoDB.
/// </summary>
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
