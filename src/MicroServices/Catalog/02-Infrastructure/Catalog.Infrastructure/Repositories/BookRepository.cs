using Catalog.ApplicationServices;
using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;
using Framework.Domain;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Infrastructure.Repositories;

class BookRepository : IBookRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly IMapper _mapper;

    public BookRepository(CatalogDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task AddBook(Book book, CancellationToken ct)
    {
        var bookData = _mapper.Map<BookData>(book);
        await _dbContext.Books.InsertOneAsync(_dbContext.Session, bookData, null, ct);
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
