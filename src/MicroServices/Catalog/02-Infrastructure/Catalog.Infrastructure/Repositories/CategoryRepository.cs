using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;

namespace Catalog.Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _dbContext;

    public CategoryRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(CategoryDto category, CancellationToken ct)
    {
        var categoryData = new CategoryData { Name = category.Name };
        await _dbContext.Categories.InsertOneAsync(categoryData, null, ct);
    }

    public Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
