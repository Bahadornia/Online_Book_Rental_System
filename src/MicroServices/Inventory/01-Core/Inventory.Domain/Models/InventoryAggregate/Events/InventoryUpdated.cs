using Framework.Domain;

namespace Inventory.Domain.Models.InventoryAggregate.Events;

public record InventoryUpdated(Entities.BookInventory BookInventory) : IDomainEvent;
