namespace Catalog.Domain.IServices;

public interface ICategoryService
{
    Task<bool> CheckIfCategoryNotExists(string name, CancellationToken ct = default);

}
