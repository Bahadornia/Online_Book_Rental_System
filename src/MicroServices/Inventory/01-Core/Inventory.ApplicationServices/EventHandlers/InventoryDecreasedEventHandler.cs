using Inventory.Domain.IServices;
using Inventory.Domain.Models.InventoryAggregate.Events;
using MediatR;

namespace Inventory.ApplicationServices.EventHandlers
{
    class InventoryDecreasedEventHandler : INotificationHandler<InventoryDecreased>
    {
        private readonly IEventPublisher _eventPublisher;

        public InventoryDecreasedEventHandler(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public async Task Handle(InventoryDecreased @event, CancellationToken ct)
        {
            await _eventPublisher.Publish(@event, ct);
        }
    }
}
