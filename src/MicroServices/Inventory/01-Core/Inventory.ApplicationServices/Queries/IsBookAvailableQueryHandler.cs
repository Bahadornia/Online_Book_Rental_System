using Framework.CQRS;
using Inventory.Domain.IRepositories;
using Inventory.Domain.IServices;

namespace Inventory.ApplicationServices.Queries;

internal class IsBookAvailableQueryHandler : IQueryHandler<IsBookAvailableQuery, bool>
{
    private readonly IInventoryService _inventoryService;

    public IsBookAvailableQueryHandler(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public async Task<bool> Handle(IsBookAvailableQuery request, CancellationToken ct)
    {
        var bookId = request.BookId;
        return await _inventoryService.IsBookAvailable(request.BookId, ct);

    }
}
