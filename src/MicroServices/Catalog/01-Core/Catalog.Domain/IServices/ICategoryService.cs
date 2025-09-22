namespace Catalog.Domain.IServices;

public interface ICategoryService
{
    Task AddIfCategoryNotExists(string name, CancellationToken ct = default);

}
