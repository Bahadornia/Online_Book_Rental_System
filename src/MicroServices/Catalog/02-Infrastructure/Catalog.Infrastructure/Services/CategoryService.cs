using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly CatalogDbContext _catalogDbContext;

    public CategoryService(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public async Task<bool> CheckIfCategoryNotExists(string name, CancellationToken ct = default)
    {
        var filter = Builders<Category>.Filter.Eq(c => c.Name, name);
        var rs = await _catalogDbContext.Categories.Find(filter).AnyAsync(ct);
        return rs;
    }
}
