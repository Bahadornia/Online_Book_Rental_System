using Inventory.Domain.IRepositories;
using MassTransit;
using SharedKernel.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Consumers
{
    class BookDeletedConsumer : IConsumer<BookDeletedIntegrationEvent>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitofWork _unitOfWork;

        public BookDeletedConsumer(IInventoryRepository inventoryRepository, IUnitofWork unitOfWork)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<BookDeletedIntegrationEvent> context)
        {
            var bookId = context.Message.BookId;
            await _inventoryRepository.DeleteInventory(bookId, context.CancellationToken);
            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }

    public class BookDeletedFaultConsumer : IConsumer<Fault<BookDeletedIntegrationEvent>>
    {
        public async Task Consume(ConsumeContext<Fault<BookDeletedIntegrationEvent>> context)
        {
            Console.WriteLine(context.Message);
            await Task.CompletedTask;
        }
    }
}
