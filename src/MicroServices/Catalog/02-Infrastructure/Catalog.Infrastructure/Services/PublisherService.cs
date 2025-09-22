using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Services;

public sealed class PublisherService : IPublisherService
{
    private readonly CatalogDbContext _dbContext;

    public PublisherService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Publisher> AddIfPublisherNotExists(string name, CancellationToken ct)
    {
        var foundedPublisher = await _dbContext.Publishers.FirstOrDefaultAsync(x => x.Name.Equals(name), ct);
        if (foundedPublisher is null)
        {
            var publisher = Publisher.Create(name);
            await _dbContext.Publishers.AddAsync(publisher, ct);
            return publisher;
        }
        return foundedPublisher;
    }
}
