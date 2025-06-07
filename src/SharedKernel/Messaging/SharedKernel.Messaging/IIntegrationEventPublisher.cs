using SharedKernel.Messaging.Events;

namespace SharedKernel.Messaging;

public interface IIntegrationEventPublisher
{
    Task Publish<T>(IntegrationBaseEvent @event, CancellationToken ct) where T:IntegrationBaseEvent;
}
