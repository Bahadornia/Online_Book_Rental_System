using Inventory.Domain.IRepositories;
using Inventory.Domain.IServices;
using Inventory.Domain.Models.InventoryAggregate.Entities;
using MassTransit;
using SharedKernel.Messaging.Events;

namespace Inventory.Infrastructure.Consumers
{
    public class BookAddedConsumer : IConsumer<BookAddedIntegrationEvent>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IInventoryRepository _inventoryRepository;

        public BookAddedConsumer(IInventoryRepository inventoryRepository, IUnitofWork unitofWork)
        {
            _inventoryRepository = inventoryRepository;
            _unitofWork = unitofWork;
        }

        public async Task Consume(ConsumeContext<BookAddedIntegrationEvent> context)
        {
            var book = context.Message;
            var bookInventory = BookInventory.Create(book.BookId, book.AvailableCopies, book.AvailableCopies);
            _inventoryRepository.AddInventory(bookInventory);
            await _unitofWork.SaveChangesAsync(context.CancellationToken);
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
