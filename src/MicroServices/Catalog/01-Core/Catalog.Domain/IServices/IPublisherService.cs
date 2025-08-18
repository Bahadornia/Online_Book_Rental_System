using MongoDB.Driver;

namespace Catalog.Domain.IServices;

public interface IPublisherService
{
    Task AddIfPublisherNotExists(IClientSessionHandle session, string name, CancellationToken ct = default);
}
