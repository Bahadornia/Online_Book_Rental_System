using Framework;
using Library.Repository.Domain.Models.BookAggregate.Entities;

namespace Library.Repository.Domain.Models.BookAggregate.Events;

public record BookAddedEvent(Book Book): IDomainEvent
{
}
