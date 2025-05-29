using Framework;

namespace Inventory.Domain.Models.InventoryAggregate.Events;

public record InventoryUpdated(Entities.BookInventory BookInventory) : IDomainEvent;
