namespace Catalog.Domain.IServices;

public interface IPublisherService
{
    Task AddIfPublisherNotExists(string name, CancellationToken ct = default);
}
