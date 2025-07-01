using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;
using MapsterMapper;
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
        var bookData = _mapper.Map<BookData
            >(book);
        await _dbContext.Books.InsertOneAsync(_dbContext.Session, bookData, null, ct);
    }

    public async Task DeleteBook(long bookId, CancellationToken ct)
    {
        var builder = Builders<BookData>.Filter;
        var filter = builder.Eq(b=> b.Id ,bookId);
        var book = _dbContext.Books.Find(filter);
        await _dbContext.Books.DeleteOneAsync(filter, ct);
    }
    
    public async Task<IReadOnlyCollection<BookDto>> GetAll(CancellationToken ct)
    {
        var builder = Builders<BookData>.Filter;
        var filter = builder.Empty;
        var rs = await _dbContext.Books.Find(filter).ToListAsync(ct);
        var result = _mapper.Map<IReadOnlyCollection<BookDto>>(rs);
        return result;
    }

    public Task<BookDto> GetBookById(long id, CancellationToken ct)
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
