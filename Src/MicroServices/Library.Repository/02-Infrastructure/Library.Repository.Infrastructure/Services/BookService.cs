using Library.Repository.Domain.Dtos;
using Library.Repository.Domain.Logics;
using Library.Repository.Domain.Models.BookAggregate.Entities;
using Library.Repository.Infrastructure.Data;
using MapsterMapper;

namespace Library.Repository.Infrastructure.Services;

class BookService : IBookSevice
{
    private readonly BookDbContext _dbContext;
    private readonly IMapper _mapper;

    public BookService(BookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task AddBook(BookDto bookDto, CancellationToken ct)
    {
        var book = _mapper.Map<Book>(bookDto);
        _dbContext.Books.InsertOne(book);
        throw new NotImplementedException();
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
