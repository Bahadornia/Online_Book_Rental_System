namespace Catalog.Domain.Dtos;

public sealed class AllBooksDto
{
    public IEnumerable<BookDto> Books { get; set; } = [];
    public long TotalCount { get; set; }
}
