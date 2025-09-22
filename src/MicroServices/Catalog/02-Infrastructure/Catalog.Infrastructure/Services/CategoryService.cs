using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly CatalogDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(CatalogDbContext catalogDbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = catalogDbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task AddIfCategoryNotExists(long id, string name, CancellationToken ct = default)
    {
        var category = Category.Create(id, name);
        _dbContext.Categories.Add(category);
        await _unitOfWork.SaveChangesAsync(ct);

    }
}
