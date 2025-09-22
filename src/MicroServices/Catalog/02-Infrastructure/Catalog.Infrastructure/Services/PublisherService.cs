using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Services;

public sealed class PublisherService : IPublisherService
{
    private readonly CatalogDbContext _dbContext;

    public PublisherService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddIfPublisherNotExists(string name, CancellationToken ct)
    {
        var publisher = Publisher.Create(name);

        await _dbContext.Publishers.AddAsync(publisher, ct);
    }
}
