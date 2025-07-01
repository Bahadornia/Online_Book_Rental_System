using Inventory.Domain.IRepositories;
using Inventory.Domain.IServices;
using Inventory.Domain.Models.InventoryAggregate.Entities;

namespace Inventory.Infrastructure.Services;

internal class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IUnitofWork _unintOfWork;

    public InventoryService(IInventoryRepository inventoryRepository, IUnitofWork unintOfWork)
    {
        _inventoryRepository = inventoryRepository;
        _unintOfWork = unintOfWork;
    }

    public async Task DecreaseAvailableCopies(long bookId, CancellationToken ct)
    {
        var bookInventory = await _inventoryRepository.GetInventory(bookId, ct);
        bookInventory.DecreaseInventory();
        _inventoryRepository.UpdateInventory(bookInventory);
        await _unintOfWork.SaveChangesAsync(ct);

    }
    public Task IncreaseAvailableCopies(long bookId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsBookAvailable(long bookId, CancellationToken ct)
    {
        var book = await _inventoryRepository.GetInventory(bookId, ct);
        return book.AvailableCopies > 0 ;
    }

    public Task UpdateTotalCopies(long bookId, int newTotal, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
