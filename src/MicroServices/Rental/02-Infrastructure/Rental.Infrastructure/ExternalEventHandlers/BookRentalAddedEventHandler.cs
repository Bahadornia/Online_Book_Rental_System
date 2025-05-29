using MediatR;
using Rental.Domain.Models.RentalAggregate.Events;

namespace Rental.Infrastructure.ExternalEventHandlers;

class BookRentalAddedEventHandler : INotificationHandler<RentalAddedEvent>
{
    public Task Handle(RentalAddedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
