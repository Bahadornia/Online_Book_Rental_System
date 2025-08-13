using Catalog.Domain.Dtos;
using Catalog.Domain.IServices;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Services;

internal class BookService : IBookService
{
    private readonly CatalogDbContext _dbContext;

    public BookService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

}
