using Catalog.ApplicationServices;
using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;
using DnsClient.Internal;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Repositories;

class BookRepository : IBookRepository
{
    private readonly BookDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<BookDbContext> _logger;

    public BookRepository(BookDbContext dbContext, IMapper mapper, ILogger<BookDbContext> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task AddBook(BookDto bookDto, CancellationToken ct)
    {
        var book = Book.Create(bookDto.Id, bookDto.Title, bookDto.Author, bookDto.PublisherId, bookDto.CategoryId, bookDto.ISBN, bookDto.Description, bookDto.Image);

        var bookData = _mapper.Map<BookData>(book);
        
        await _dbContext.Books.InsertOneAsync(bookData, null, ct);
        _logger.LogAddBook(book.Id.Value);
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
