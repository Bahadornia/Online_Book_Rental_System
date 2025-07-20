using Framework.Domain;
using Order.Domain.Models.RentalAggregate.Entities;

namespace Order.Domain.Models.RentalAggregate.Events;

public record OrderUpdatedEvent(BookOrder rental): IDomainEvent;
