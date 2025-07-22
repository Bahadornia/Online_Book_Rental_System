using Framework.Domain;
using Order.Domain.Models.OrderAggregate.Entities;

namespace Order.Domain.Models.OrderAggregate.Events;

public record OrderUpdatedEvent(BookOrder rental): IDomainEvent;
