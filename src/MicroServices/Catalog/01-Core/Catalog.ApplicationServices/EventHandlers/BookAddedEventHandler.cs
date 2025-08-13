using Catalog.Domain.Models.BookAggregate.Events;
using Framework.SnowFlake;
using MediatR;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;

namespace Catalog.ApplicationServices.EventHandlers;

internal sealed class BookAddedEventHandler : INotificationHandler<BookAdded>
{
    private readonly IIntegrationEventPublisher _eventPublisher;
    private readonly ISnowFlakeService _snowFlakeService;

    public BookAddedEventHandler(IIntegrationEventPublisher eventPublisher, ISnowFlakeService snowFlakeService)
    {
        _eventPublisher = eventPublisher;
        _snowFlakeService = snowFlakeService;
    }

    public async Task Handle(BookAdded notification, CancellationToken ct)
    {
        var bookAddedEvent = new BookAddedIntegrationEvent
        {
            CorrelationId = Guid.NewGuid(),
            BookId = notification.Book.Id.Value,
            AvailableCopies = notification.Book.AvailableCopies,
        };
        await _eventPublisher.Publish<BookAddedIntegrationEvent>(bookAddedEvent, ct);
    }
}
