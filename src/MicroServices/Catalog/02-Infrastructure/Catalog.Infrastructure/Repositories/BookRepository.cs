using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Catalog.Infrastructure.Repositories;

internal class BookRepository : IBookRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly IMapper _mapper;

    public BookRepository(CatalogDbContext dbContext, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Add(Book book)
    {
        _dbContext.Books.Add(book);
        
    }

    public void Delete(Book book)
    {
        _dbContext.Books.Remove(book);
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

        var result = new AllBooksDto
        {
            Books = _mapper.Map<IReadOnlyCollection<BookDto>>(rs),
            TotalCount = totalCount,
        };
        return result;
    }

    public async Task<Book> GetById(long id, CancellationToken ct)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id, ct);
        return book;
    }

    public async Task<IReadOnlyCollection<Book>> GetByIds(IEnumerable<long> ids, CancellationToken ct)
    {
        var books = await _dbContext.Books.Where(b => ids.Contains(b.Id)).ToListAsync(ct);
        return books;
    }

    public async Task<IReadOnlyCollection<BookDto>> Search(IQueryable<Book> books, BookFilterDto filterDto, CancellationToken ct)
    {
        var query = books.Where(b => true);

        if (!string.IsNullOrWhiteSpace(filterDto.Title))
        {
            query = query.Where(b => b.Title.Contains(filterDto.Title));
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

        var result = await query.ToListAsync(ct);
        return _mapper.Map<IReadOnlyCollection<BookDto>>(result);
    }

    public Task Update(BookDto book, CancellationToken ct)
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