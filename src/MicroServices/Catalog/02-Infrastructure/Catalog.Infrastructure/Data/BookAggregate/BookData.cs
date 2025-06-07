using Catalog.Domain.Models.BookAggregate.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Infrastructure.Data.BookAggregate;

public class BookData
{
    
    public long Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = default!;

    [BsonElement("author")]
    public string Author { get; set; } = default!;

    [BsonElement("publisher")]
    public PublisherData Publisher { get; set; } = default!;

    [BsonElement("category")]
    public CategoryData Category { get; set; } = default!;

    [BsonElement("isbn")]
    public long ISBN { get; set; } = default!;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("imageUrl")]
    public string? ImageUrl { get; set; }

    [BsonElement("availableCopies")]
    public int AvailableCopies { get; set; }

}
