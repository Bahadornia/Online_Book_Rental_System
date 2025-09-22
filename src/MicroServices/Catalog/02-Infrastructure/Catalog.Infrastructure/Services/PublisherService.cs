using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Services;

public sealed class PublisherService : IPublisherService
{
    private readonly CatalogDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public PublisherService(CatalogDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task AddIfPublisherNotExists(long id, string name, CancellationToken ct)
    {
        var publisher = Publisher.Create(id, name);

        _dbContext.Publishers.Add(publisher);
        await _unitOfWork.SaveChangesAsync(ct);
    }
}
