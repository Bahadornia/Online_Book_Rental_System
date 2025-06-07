using MassTransit;
using SharedKernel.Messaging.Events;

namespace SharedKernel.Messaging;

internal class IntegrationEventPulbisher : IIntegrationEventPublisher
{
    private readonly IPublishEndpoint _publishEndPoint;

    public IntegrationEventPulbisher(IPublishEndpoint publishEndPoint)
    {
        _publishEndPoint = publishEndPoint;
    }

    public async Task Publish<T>(IntegrationBaseEvent @event, CancellationToken ct)
        where T: IntegrationBaseEvent

    {
        await _publishEndPoint.Publish<T>(@event, ct);
    }
}

