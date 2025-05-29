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

    public async Task AddBookToInventory(long bookId, int initialCopies, CancellationToken ct)
    {
        var bookInventory = BookInventory.Create(bookId, initialCopies, initialCopies);
        _inventoryRepository.AddBooToInventory(bookInventory);
        await _unintOfWork.SaveChangesAsync(ct);
    }

    public async Task DecreaseAvailableCopies(long bookId, CancellationToken ct)
    {
        var bookInventory = await _inventoryRepository.GetInventory(bookId, ct);
        bookInventory.DecreaseInventory();
        _inventoryRepository.UpdateInventory(bookInventory);
        await _unintOfWork.SaveChangesAsync(ct);

    }

    public Task<BookInventory> GetInventoryAsync(long bookId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task IncreaseAvailableCopies(long bookId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTotalCopies(long bookId, int newTotal, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
