using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly CatalogDbContext _dbContext;

    public CategoryService(CatalogDbContext catalogDbContext)
    {
        _dbContext = catalogDbContext;
    }

    public async Task<Category> AddIfCategoryNotExists(string name, CancellationToken ct)
    {
        var foundedCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name.Contains(name), ct);
        if (foundedCategory is null)
        {
            var category = new Category
            {
                Name = name,
            };
            await _dbContext.Categories.AddAsync(category, ct);
            return category;
        }
        return foundedCategory;
    }
}
