using Framework;
using Inventory.Domain.IServices;
using MassTransit;

namespace Inventory.Infrastructure.Services;

internal class EventPulbisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndPoint;

    public EventPulbisher(IPublishEndpoint publishEndPoint)
    {
        _publishEndPoint = publishEndPoint;
    }

    public async Task Publish(IDomainEvent @event, CancellationToken ct)
    {
        await _publishEndPoint.Publish(@event, ct);
    }
}

