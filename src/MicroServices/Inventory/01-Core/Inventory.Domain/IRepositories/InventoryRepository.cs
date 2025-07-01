using Inventory.Domain.Models.InventoryAggregate.Entities;
namespace Inventory.Domain.IRepositories;

    public interface IInventoryRepository
    {
    void AddInventory(BookInventory bookInventory);
    void UpdateInventory(BookInventory bookInventory);
    Task<BookInventory> GetInventory(long bookId, CancellationToken ct);
    }

