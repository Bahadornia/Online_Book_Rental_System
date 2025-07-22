using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories;

public interface IBookRepository
{
    Task AddBook(Book book, CancellationToken ct);
    Task UpdateBook(BookDto book, CancellationToken ct);
    Task DeleteBook(long bookId, CancellationToken ct);
    Task<IReadOnlyCollection<BookDto>> SearchBook(BookFilterDto filter,  CancellationToken ct);
    Task<BookDto> GetBookById(long id, CancellationToken ct);
    Task<IReadOnlyCollection<BookDto>> GetAll(CancellationToken ct);
    Task<IReadOnlyCollection<BookDto>> GetBooksByIds(IEnumerable<long> ids, CancellationToken ct);
}
