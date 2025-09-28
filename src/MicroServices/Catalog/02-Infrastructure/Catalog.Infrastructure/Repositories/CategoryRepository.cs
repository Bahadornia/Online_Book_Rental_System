using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _dbContext;

    public CategoryRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
        };
        _dbContext.Categories.Add(category);
    }

    public async Task<IReadOnlyCollection<Category>> GetAll(string term, CancellationToken ct)
    {
        var categories = await _dbContext.Categories.Where(c => c.Name.Contains(term)).AsNoTracking().ToListAsync(ct);
        return categories.AsReadOnly();
    }
}
