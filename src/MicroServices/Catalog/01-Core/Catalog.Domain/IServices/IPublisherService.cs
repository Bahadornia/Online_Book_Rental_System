using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IServices;

public interface IPublisherService
{
    Task<Publisher> AddIfPublisherNotExists(string name, CancellationToken ct = default);
}
