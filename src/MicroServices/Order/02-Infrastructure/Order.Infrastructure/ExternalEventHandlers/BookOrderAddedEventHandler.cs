using MediatR;
using Order.Domain.Models.OrderAggregate.Events;

namespace Order.Infrastructure.ExternalEventHandlers;

class BookRentalAddedEventHandler : INotificationHandler<OrderAddedEvent>
{
    public Task Handle(OrderAddedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
