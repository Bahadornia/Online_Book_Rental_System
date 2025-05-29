using Inventory.Domain.IServices;
using MassTransit;
using SharedKernel.Messaging.Events;

namespace Inventory.Infrastructure.Consumers;

class BookRentedConsumer : IConsumer<BookRented>
{
    private readonly IInventoryService _inventoryService;

    public BookRentedConsumer(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public async Task Consume(ConsumeContext<BookRented> context)
    {
        var bookId = context.Message.BookId;
        await _inventoryService.DecreaseAvailableCopies(bookId, context.CancellationToken);
    }

}
