using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Services;

public sealed class PublisherService : IPublisherService
{
    private readonly CatalogDbContext _dbContext;

    public PublisherService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CheckIfPublisherNotExists(string name, CancellationToken ct = default)
    {
        var filter = Builders<Publisher>.Filter.Eq(p => p.Name, name);
        var rs = await _dbContext.Publishers.Find(filter).AnyAsync(ct);
        return rs;
    }
}
