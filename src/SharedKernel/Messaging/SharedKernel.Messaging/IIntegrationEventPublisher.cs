using SharedKernel.Messaging.Events;

namespace SharedKernel.Messaging;

public interface IIntegrationEventPublisher
{
    Task Publish(IntegrationBaseEvent @event, CancellationToken ct);
}
