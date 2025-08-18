using MongoDB.Driver;

namespace Catalog.Domain.IServices;

public interface ICategoryService
{
    Task AddIfCategoryNotExists(IClientSessionHandle session,string name, CancellationToken ct = default);

}
