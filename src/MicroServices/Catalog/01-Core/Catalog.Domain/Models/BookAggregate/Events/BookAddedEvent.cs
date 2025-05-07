using Framework;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.Models.BookAggregate.Events;

public record BookAddedEvent(Book Book): IDomainEvent
{
}
