using Catalog.Domain.Models.BookAggregate.Entities;
using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.Events;

public record BookAdded(Book Book): IDomainEvent;
