using Catalog.ApplicationServices;
using Catalog.Domain.Dtos;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        var book = Book.Create(bookDto.Id, bookDto.Title, bookDto.Author, bookDto.PublisherId, bookDto.CategoryId, bookDto.ISBN, bookDto.Description, bookDto.ImageUrl);

        var bookData = _mapper.Map<BookData>(book);

        await _dbContext.Books.InsertOneAsync(bookData, null, ct);
        _logger.LogAddBook(book.Id.Value);
    }

    public Task DeleteBook(BookDto book, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<BookDto>> GetAll(CancellationToken ct)
    {
        var builder = Builders<BookData>.Filter;
        var filter = builder.Empty;
        var rs = await _dbContext.Books.Find(filter).ToListAsync(ct);
        var result = _mapper.Map<IReadOnlyCollection<BookDto>>(rs);
        return result;
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
