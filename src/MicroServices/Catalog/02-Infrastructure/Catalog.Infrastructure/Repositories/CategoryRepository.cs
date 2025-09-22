using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryRepository(CatalogDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(CategoryDto dto, CancellationToken ct)
    {
        var category = Category.Create(dto.Id, dto.Name);
        _dbContext.Categories.Add(category);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
