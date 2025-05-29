using Inventory.Domain.Models.InventoryAggregate.Entities;

namespace Inventory.Domain.IServices;

public interface IInventoryService
{
    Task AddBookToInventory(long bookId, int initialCopies, CancellationToken ct);
    Task UpdateTotalCopies(long bookId, int newTotal, CancellationToken ct); // For restocking or reducing inventory

    Task DecreaseAvailableCopies(long bookId, CancellationToken ct); // On rent
    Task IncreaseAvailableCopies(long bookId, CancellationToken ct); // On return

    Task<BookInventory> GetInventoryAsync(long bookId, CancellationToken ct); // For queries (optional)
}
