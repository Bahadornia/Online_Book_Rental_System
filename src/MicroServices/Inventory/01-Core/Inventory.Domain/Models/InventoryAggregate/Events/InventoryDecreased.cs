using Framework;
using Inventory.Domain.Models.InventoryAggregate.Entities;

namespace Inventory.Domain.Models.InventoryAggregate.Events;

public record InventoryDecreased(BookInventory BookInventory) : IDomainEvent;

