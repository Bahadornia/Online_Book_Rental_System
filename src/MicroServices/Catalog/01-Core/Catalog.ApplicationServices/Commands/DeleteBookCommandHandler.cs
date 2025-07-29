using Catalog.Domain.IRepositories;
using Framework.CQRS;
using Framework.SnowFlake;
using MediatR;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;

namespace Catalog.ApplicationServices.Commands;

public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IIntegrationEventPublisher _eventPublisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISnowFlakeService _snowFlakeService;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork, IIntegrationEventPublisher eventPublisher, ISnowFlakeService snowFlakeService)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _snowFlakeService = snowFlakeService;
    }

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken ct)
    {
        await _unitOfWork.BeginTransaction(ct);
        await _bookRepository.DeleteBook(request.BookId, ct);
        var bookDeletedEvent = new BookDeletedIntegrationEvent
        {
            BookId = request.BookId,
            EventId = _snowFlakeService.CreateId(),
        };
        await _eventPublisher.Publish<BookDeletedIntegrationEvent>(bookDeletedEvent, ct);
        try
        {
            await _unitOfWork.CommitTransaction(ct);
        }
        catch (Exception)
        {

            await _unitOfWork.AbortTransaction(ct);
        }
        return Unit.Value;
    }
}
