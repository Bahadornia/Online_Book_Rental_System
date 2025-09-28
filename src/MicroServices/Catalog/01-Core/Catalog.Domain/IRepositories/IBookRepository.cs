using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories;

public interface IBookRepository
{
    void Add(Book book);
    Task Update(BookDto book, CancellationToken ct);
    void Delete(Book book);
    Task<IReadOnlyCollection<BookDto>> Search(IQueryable<Book> books, BookFilterDto filter,  CancellationToken ct);
    Task<Book> GetById(long id, CancellationToken ct);
    Task<AllBooksDto> GetAll(AgGridRequestDto request,CancellationToken ct);
    Task<IReadOnlyCollection<Book>> GetByIds(IEnumerable<long> ids, CancellationToken ct);
}
