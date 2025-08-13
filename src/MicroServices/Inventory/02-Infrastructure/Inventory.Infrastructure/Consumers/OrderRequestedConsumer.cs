using Inventory.Domain.IServices;
using MassTransit;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;

namespace Inventory.Infrastructure.Consumers;

class OrderRequestedConsumer : IConsumer<OrderRequested>
{
    private readonly IInventoryService _inventoryService;
    private readonly IIntegrationEventPublisher _publisher;

    public OrderRequestedConsumer(IInventoryService inventoryService, IIntegrationEventPublisher publisher)
    {
        _inventoryService = inventoryService;
        _publisher = publisher;
    }

    public async Task Consume(ConsumeContext<OrderRequested> context)
    {
        var bookId = context.Message.BookId;
        await _inventoryService.DecreaseAvailableCopies(bookId, context.CancellationToken);
        await _publisher.Publish<BookReserved>(new BookReserved { CorrelationId = context.Message.CorrelationId, BookId = bookId }, context.CancellationToken);
    }

}
