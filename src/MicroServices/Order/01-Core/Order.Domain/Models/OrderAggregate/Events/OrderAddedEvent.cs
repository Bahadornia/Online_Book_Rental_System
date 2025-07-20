using Framework.Domain;
using Order.Domain.Models.RentalAggregate.Entities;

namespace Order.Domain.Models.RentalAggregate.Events;

public record OrderAddedEvent(BookOrder Rental) : IDomainEvent;

