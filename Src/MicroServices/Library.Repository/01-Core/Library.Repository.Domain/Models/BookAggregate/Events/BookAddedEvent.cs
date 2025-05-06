using Framework;

namespace Library.Repository.Domain.Models.BookAggregate.Events;

public record BookAddedEvent(Guid Id): IDomainEvent
{
}
