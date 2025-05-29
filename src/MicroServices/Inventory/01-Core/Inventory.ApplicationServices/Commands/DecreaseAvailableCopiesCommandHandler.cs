using Framework.CQRS;
using Inventory.Domain.IServices;
using MediatR;

namespace Inventory.ApplicationServices.Commands;

public class DecreaseAvailableCopiesCommandHandler : ICommandHandler<DecreaseAvailableCopiesCommand>
{
    private readonly IInventoryService _inventoryService;

    public DecreaseAvailableCopiesCommandHandler(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public async Task<Unit> Handle(DecreaseAvailableCopiesCommand command, CancellationToken ct)
    {
        await _inventoryService.DecreaseAvailableCopies(command.BookId, ct);
        return Unit.Value;
    }
}
