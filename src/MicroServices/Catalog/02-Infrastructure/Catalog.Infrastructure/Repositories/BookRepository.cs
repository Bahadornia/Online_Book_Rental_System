using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;
using MapsterMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text.RegularExpressions;

namespace Catalog.Infrastructure.Repositories;

internal class BookRepository : IBookRepository
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

    public async Task<BookDto> GetBookById(long id, CancellationToken ct)
    {
        var filterBuilder = Builders<BookData>.Filter;
        var filter = filterBuilder.Eq(b => b.Id, id);
        var book = await _dbContext.Books.Find(filter, null).FirstOrDefaultAsync(ct);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IReadOnlyCollection<BookDto>> GetBooksByIds(IEnumerable<long> ids, CancellationToken ct)
    {
        var filterBuilder = Builders<BookData>.Filter;
        var filter = filterBuilder.In(b => b.Id, ids);
        var books = await _dbContext.Books.Find(filter, null).ToListAsync(ct);
        
       return  _mapper.Map<IReadOnlyCollection<BookDto>>(books);
       
    }

    public async Task<IReadOnlyCollection<BookDto>> SearchBook(BookFilterDto filterDto, CancellationToken ct)
    {
        var builder = Builders<BookData>.Filter;
        var filters = new List<FilterDefinition<BookData>>();
        if (!string.IsNullOrWhiteSpace(filterDto.Title))
        {
            filters.Add(builder.Regex(item => item.Title, new BsonRegularExpression(filterDto.Title, "i")));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.Author))
        {
            filters.Add(builder.Regex(item => item.Author, new BsonRegularExpression(filterDto.Author, "i")));
        } 
        if (!string.IsNullOrWhiteSpace(filterDto.Publisher))
        {
            filters.Add(builder.Regex(item => item.Publisher, new BsonRegularExpression(filterDto.Publisher, "i")));
        } 
        if (!string.IsNullOrWhiteSpace(filterDto.Category))
        {
            filters.Add(builder.Regex(item => item.Category, new BsonRegularExpression(filterDto.Category, "i")));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.ISBN))
        {
            filters.Add(builder.Regex(item => item.ISBN, new BsonRegularExpression("^" + Regex.Escape(filterDto.ISBN), "i")));
        }

        var filter = filters.Count != 0 ? builder.Or(
          filters
            ) : builder.Empty;
        var rs=  await _dbContext.Books.Find(filter).ToListAsync(ct);
        return _mapper.Map<IReadOnlyCollection<BookDto>>(rs);
    }

    public Task UpdateBook(BookDto book, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
