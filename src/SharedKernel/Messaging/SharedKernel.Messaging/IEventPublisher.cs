using SharedKernel.Messaging.Events;

namespace SharedKernel.Messaging;

public interface IIntegrationEventPublisher
{
    Task Publish(IIntegrationEvent @event, CancellationToken ct);
}
