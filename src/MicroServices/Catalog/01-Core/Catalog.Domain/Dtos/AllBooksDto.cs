namespace Catalog.Domain.Dtos;

public sealed class AllBooksDto
{
    public IReadOnlyCollection<BookDto> Books { get; set; } = [];
    public long TotalCount { get; set; }
}
