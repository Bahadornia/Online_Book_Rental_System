using Inventory.Domain.Models.InventoryAggregate.Entities;

namespace Inventory.Domain.IServices;

public interface IInventoryService
{
    Task DecreaseAvailableCopies(long bookId, CancellationToken ct); // On rent
    Task IncreaseAvailableCopies(long bookId, CancellationToken ct); // On return
    Task<bool> IsBookAvailable(long bookId, CancellationToken ct);
}
