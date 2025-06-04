using Catalog.Domain.Models.BookAggregate.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Infrastructure.Data.BookAggregate;

public class BookData
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public int PublisherId { get; set; }
    public int CategoryId { get; set; }
    public long ISBN { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int AvailableCopies { get; set; }

}
