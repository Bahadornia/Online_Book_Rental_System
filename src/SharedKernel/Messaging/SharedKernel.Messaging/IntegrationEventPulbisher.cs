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

    public async Task Publish(IntegrationBaseEvent @event, CancellationToken ct)
       
    {
        await _publishEndPoint.Publish(@event, ct);
    }
}

