using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public class CategoryData
{
    [BsonElement("name")]
    public string Name { get; set; } = default!;
};
