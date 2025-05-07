using Catalog.Domain.Models.BookAggregate.ValueObjects;

namespace Catalog.Domain.Dtos;

public record BookDto
{
    public Guid Id { get; set; }
    public string Title { get; init; } = default!;
    public string Author { get; init; } = default!;
    public int PublisherId { get; init; }
    public int CategoryId { get; init; }
    public long ISBN { get; init; }
    public string Description { get; init; } = default!;
    public string Image { get; set; } = default!;
};
