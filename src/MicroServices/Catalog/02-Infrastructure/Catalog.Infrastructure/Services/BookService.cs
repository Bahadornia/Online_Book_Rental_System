using Catalog.Domain.Dtos;
using Catalog.Domain.Logics;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Domain.Models.BookAggregate.Events;
using Catalog.Infrastructure.Data;
using MapsterMapper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Services;

class BookService : IBookSevice
{
    private readonly BookDbContext _dbContext;
    private readonly IMapper _mapper;

    public BookService(BookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task AddBook(BookDto bookDto, CancellationToken ct)
    {
        var book = Book.Create(bookDto.Id, bookDto.Title, bookDto.Author, bookDto.PublisherId, bookDto.CategoryId, bookDto.ISBN, bookDto.Description, bookDto.Image);

            await _dbContext.Books.InsertOneAsync(book, null, ct);
    }

    public Task DeleteBook(BookDto book, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<BookDto>> GetAll(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<BookDto> GetBookById(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<BookDto>> SearchBook(BookFilterDto filter, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBook(BookDto book, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
