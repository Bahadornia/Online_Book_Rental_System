using Library.Repository.Domain.Dtos;

namespace Library.Repository.Domain.Logics;

public interface IBookSevice
{
    Task AddBook(BookDto book, CancellationToken ct);
    Task UpdateBook(BookDto book, CancellationToken ct);
    Task DeleteBook(BookDto book, CancellationToken ct);
    Task<IReadOnlyCollection<BookDto>> SearchBook(BookFilterDto filter, CancellationToken ct);
    Task<BookDto> GetBookById(Guid id, CancellationToken ct);
    Task<IReadOnlyCollection<BookDto>> GetAll(Guid id, CancellationToken ct);
}
