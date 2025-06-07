using Inventory.Domain.Models.InventoryAggregate.Events;
using MediatR;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;

namespace Inventory.ApplicationServices.EventHandlers
{
    class InventoryDecreasedEventHandler : INotificationHandler<InventoryDecreased>
    {
        private readonly IIntegrationEventPublisher _eventPublisher;

        public InventoryDecreasedEventHandler(IIntegrationEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public async Task Handle(InventoryDecreased @event, CancellationToken ct)
        {
            await _eventPublisher.Publish<IntegrationBaseEvent>(null, ct);
        }
    }
}
