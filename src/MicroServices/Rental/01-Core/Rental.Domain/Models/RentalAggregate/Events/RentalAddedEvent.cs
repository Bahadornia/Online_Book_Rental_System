using Framework;
using Rental.Domain.Models.RentalAggregate.Entities;

namespace Rental.Domain.Models.RentalAggregate.Events;

public record RentalAddedEvent(BookRental Rental) : IDomainEvent;

