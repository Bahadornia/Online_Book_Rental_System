using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly CatalogDbContext _dbContext;

    public CategoryService(CatalogDbContext catalogDbContext)
    {
        _dbContext = catalogDbContext;
    }

    public async Task AddIfCategoryNotExists(IClientSessionHandle session, string name, CancellationToken ct = default)
    {
        var filter = Builders<CategoryData>.Filter.Eq(p => p.Name, name);
        var update = Builders<CategoryData>.Update.SetOnInsert(p => p.Name, name);

        var rs = await _dbContext.Categories.UpdateOneAsync(session, filter, update, new UpdateOptions { IsUpsert = true }, ct);
    }
}
