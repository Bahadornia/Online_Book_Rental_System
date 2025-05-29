using Framework;
using Rental.Domain.Models.RentalAggregate.Entities;

namespace Rental.Domain.Models.RentalAggregate.Events;

public record RentalUpdatedEvent(BookRental rental): IDomainEvent;
