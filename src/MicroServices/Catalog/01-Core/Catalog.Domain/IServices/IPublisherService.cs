namespace Catalog.Domain.IServices;

public interface IPublisherService
{
    Task AddIfPublisherNotExists(long id, string name, CancellationToken ct = default);
}
