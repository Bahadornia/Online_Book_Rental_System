using Catalog.Domain.Models.BookAggregate.ValueObjects;

namespace Catalog.Domain.Dtos;

public record BookDto(Guid Id,
    string Title,
    string Author,
    int PublisherId,
    int CategoryId,
    long ISBN,
    string Description,
    string Image);

