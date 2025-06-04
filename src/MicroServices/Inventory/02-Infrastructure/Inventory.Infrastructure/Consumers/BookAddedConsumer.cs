using Inventory.Domain.IServices;
using MassTransit;
using SharedKernel.Messaging.Events;

namespace Inventory.Infrastructure.Consumers
{
    public class BookAddedConsumer : IConsumer<BookAddedIntegrationEvent>
    {
        private readonly IInventoryService _inventoryService;

        public BookAddedConsumer(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task Consume(ConsumeContext<BookAddedIntegrationEvent> context)
        {
            var book = context.Message;
            await _inventoryService.AddBookToInventory(book.BookId, book.AvailableCopies, context.CancellationToken);
        }
    }

    public class BookAddedFualtConsumer : IConsumer<Fault<BookAddedIntegrationEvent>>
    {
        public Task Consume(ConsumeContext<Fault<BookAddedIntegrationEvent>> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}
