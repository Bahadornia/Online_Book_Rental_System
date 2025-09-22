namespace Catalog.Domain.IServices;

public interface ICategoryService
{
    Task AddIfCategoryNotExists(long id, string name, CancellationToken ct = default);

}
