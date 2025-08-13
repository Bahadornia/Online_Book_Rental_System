using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _dbContext;

    public CategoryRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Category category, CancellationToken ct)
    {
        await _dbContext.Categories.InsertOneAsync(category, null, ct);
    }

    public Task<IReadOnlyCollection<Category>> GetAll(string category, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
