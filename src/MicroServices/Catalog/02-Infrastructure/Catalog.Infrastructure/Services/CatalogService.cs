using Catalog.Domain.Dtos;
using Catalog.Domain.IServices;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Services;

internal class CatalogService : ICatalogService
{
    private readonly CatalogDbContext _dbContext;

    public CatalogService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddBook(BookDto book, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
