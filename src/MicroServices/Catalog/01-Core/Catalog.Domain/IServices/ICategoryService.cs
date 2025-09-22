using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IServices;

public interface ICategoryService
{
    Task<Category> AddIfCategoryNotExists(string name, CancellationToken ct = default);

}
