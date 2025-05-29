using Inventory.Domain.IRepositories;
using Inventory.Domain.Models.InventoryAggregate.Entities;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories;

internal class InventoryRepository : IInventoryRepository
{

    private readonly IUnitofWork _unitOfWiork;
    private readonly InventoryDbContext _dbContext;

    public InventoryRepository(IUnitofWork unitOfWiork, InventoryDbContext inventoryDbContext)
    {
        _unitOfWiork = unitOfWiork;
        _dbContext = inventoryDbContext;
    }

    public void AddBooToInventory(BookInventory bookInventory)
    {
        _dbContext.Inventories.Add(bookInventory);
    }

    public Task<BookInventory> GetInventory(long bookId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public void UpdateInventory(BookInventory bookInventory)
    {
        _dbContext.Inventories.Update(bookInventory);
    }
}
