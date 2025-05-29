using Framework;

namespace Inventory.Domain.IServices;

public interface IEventPublisher
{
    Task Publish(IDomainEvent @event, CancellationToken ct);
}
