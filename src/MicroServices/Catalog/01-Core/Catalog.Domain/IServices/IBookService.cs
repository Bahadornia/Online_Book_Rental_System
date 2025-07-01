using Catalog.Domain.Dtos;

namespace Catalog.Domain.IServices;

public interface IBookService
{
    Task AddBook(BookDto book, CancellationToken ct = default);
}
