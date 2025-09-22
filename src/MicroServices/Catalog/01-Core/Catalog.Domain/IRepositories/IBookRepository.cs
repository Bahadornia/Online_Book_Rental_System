using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories;

public interface IBookRepository
{
    void AddBook(Book book, CancellationToken ct);
    Task UpdateBook(BookDto book, CancellationToken ct);
    void DeleteBook(long bookId, CancellationToken ct);
    Task<IReadOnlyCollection<BookDto>> SearchBook(IQueryable<Book> books, BookFilterDto filter,  CancellationToken ct);
    Task<BookDto> GetBookById(long id, CancellationToken ct);
    Task<AllBooksDto> GetAll(AgGridRequestDto request,CancellationToken ct);
    Task<IReadOnlyCollection<BookDto>> GetBooksByIds(IEnumerable<long> ids, CancellationToken ct);
}
