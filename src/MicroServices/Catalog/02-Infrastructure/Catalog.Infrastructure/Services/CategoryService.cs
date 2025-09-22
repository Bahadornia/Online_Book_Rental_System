using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly CatalogDbContext _dbContext;

    public CategoryService(CatalogDbContext catalogDbContext)
    {
        _dbContext = catalogDbContext;
    }

    public async Task AddIfCategoryNotExists(string name, CancellationToken ct)
    {
        var category = Category.Create(name);
        await _dbContext.Categories.AddAsync(category, ct);
    }
}
