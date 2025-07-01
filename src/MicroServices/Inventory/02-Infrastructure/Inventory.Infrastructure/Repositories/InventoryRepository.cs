using Inventory.Domain.IRepositories;
using Inventory.Domain.Models.InventoryAggregate.Entities;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

    public void AddInventory(BookInventory bookInventory)
    {
        _dbContext.Inventories.Add(bookInventory);
    }

    public async Task<BookInventory> GetInventory(long bookId, CancellationToken ct)
    {
        var bookInventory = await _dbContext.Inventories.FirstOrDefaultAsync(item => item.Id == bookId, ct);

        if(bookInventory is null)
        {
            throw new Exception("BookInventory not found!");
        }
        return bookInventory;
    }

    public void UpdateInventory(BookInventory bookInventory)
    {
        _dbContext.Inventories.Update(bookInventory);
    }
}
