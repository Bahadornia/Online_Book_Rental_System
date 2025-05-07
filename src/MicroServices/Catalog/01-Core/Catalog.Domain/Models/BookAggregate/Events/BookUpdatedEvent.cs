using Catalog.Domain.Models.BookAggregate.Entities;
using Framework;

namespace Catalog.Domain.Models.BookAggregate.Events
{
    public record BookUpdatedEvent(Book book): IDomainEvent;

}