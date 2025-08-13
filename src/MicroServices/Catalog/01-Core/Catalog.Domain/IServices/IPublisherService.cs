namespace Catalog.Domain.IServices;

public interface IPublisherService
{
    Task<bool> CheckIfPublisherNotExists(string name, CancellationToken ct = default);
}
