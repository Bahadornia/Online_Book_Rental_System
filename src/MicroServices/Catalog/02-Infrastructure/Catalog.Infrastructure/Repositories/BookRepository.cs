using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Framework.Exceptions;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace Catalog.Infrastructure.Repositories;

internal class BookRepository : IBookRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookRepository(CatalogDbContext dbContext, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task AddBook(Book book, CancellationToken ct)
    {
        _dbContext.Books.Add(book);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task DeleteBook(long bookId, CancellationToken ct)
    {
        var book = _dbContext.Books.Find(bookId);
        if (book is null) throw new NotFoundException(bookId, nameof(Book));
        _dbContext.Books.Remove(book);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<AllBooksDto> GetAll(AgGridRequestDto request, CancellationToken ct)
    {
        int pageSize = request.EndRow - request.StartRow;
        int pageNumber = request.EndRow / pageSize - 1;

        var totalCount = await _dbContext.Books.CountAsync(ct);
        var rs = await _dbContext.Books.AsNoTracking()
            .Skip(pageNumber)
            .Take(pageSize)
            .DynamicSort(request)
            .ToListAsync(ct);
        ;

        //if (request.FilterModel is not null &&
        //   request.SortModel is not null &&
        //   request.FilterModel.Count == 0 &&
        //   request.SortModel.Count == 0)
        //{


        //}

        var result = new AllBooksDto
        {
            Books = _mapper.Map<IReadOnlyCollection<BookDto>>(rs),
            TotalCount = totalCount,
        };
        return result;
    }

    public async Task<BookDto> GetBookById(long id, CancellationToken ct)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(b=> b.Id == id,ct);
        if(book is null) throw new NotFoundException(id, nameof(Book));
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IReadOnlyCollection<BookDto>> GetBooksByIds(IEnumerable<long> ids, CancellationToken ct)
    {
        var books = await _dbContext.Books.Where(b=> ids.Contains(b.Id)).ToListAsync(ct);

        return _mapper.Map<IReadOnlyCollection<BookDto>>(books);

    }

    public async Task<IReadOnlyCollection<BookDto>> SearchBook(IQueryable<Book> books, BookFilterDto filterDto, CancellationToken ct)
    {
        var query = books.Where(b => true);
        
        if (!string.IsNullOrWhiteSpace(filterDto.Title))
        {
            query = query.Where(b =>b.Title.Contains(filterDto.Title));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.Author))
        {
            query = query.Where(b => b.Author.Contains(filterDto.Author));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.Publisher))
        {
            query = query.Where(b => b.Author.Contains(filterDto.Publisher));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.Category))
        {
            query = query.Where(b => b.Author.Contains(filterDto.Category));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.ISBN))
        {
            query = query.Where(b => b.Author.Contains(filterDto.ISBN));
        }

       
        return _mapper.Map<IReadOnlyCollection<BookDto>>(books.ToListAsync(ct));
    }

    public Task UpdateBook(BookDto book, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}


internal static class SortBook
{
    public static IQueryable<Book> DynamicSort(this IQueryable<Book> paginateData, AgGridRequestDto request)
    {
        if (request.SortModel is { Count: > 0 } sortModels)
        {
            var firstExpression = GenerateExpression(sortModels.First());
            var orderDefinition = paginateData.OrderBy(firstExpression);
            foreach (var sortModel in sortModels)
            {

                ChainSort(ref orderDefinition, sortModel);

            }
            return orderDefinition;
        }
        return paginateData;

    }

    private static void ChainSort(ref IOrderedQueryable<Book> orderDefinition, AgSortModelDto sortModel)
    {
        var exp = GenerateExpression(sortModel);
        orderDefinition = sortModel.Sort == "asc" ?
            orderDefinition.ThenBy(exp)
            : orderDefinition.ThenByDescending(exp);
    }

    private static Expression<Func<Book, object>> GenerateExpression(AgSortModelDto sortModel)
    {
        var param = Expression.Parameter(typeof(Book), "b");
        var property = Expression.PropertyOrField(param, sortModel.ColId);
        var convert = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<Book, object>>(convert, param);
    }
}