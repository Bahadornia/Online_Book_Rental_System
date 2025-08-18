using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Services;

public sealed class PublisherService : IPublisherService
{
    private readonly CatalogDbContext _dbContext;

    public PublisherService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddIfPublisherNotExists(IClientSessionHandle session, string name, CancellationToken ct)
    {
        var filter = Builders<PublisherData>.Filter.Eq(p => p.Name, name);
        var update = Builders<PublisherData>.Update.SetOnInsert(p => p.Name, name);

        var rs = await _dbContext.Publishers.UpdateOneAsync(session, filter, update, new UpdateOptions { IsUpsert = true }, ct);
    }
}
