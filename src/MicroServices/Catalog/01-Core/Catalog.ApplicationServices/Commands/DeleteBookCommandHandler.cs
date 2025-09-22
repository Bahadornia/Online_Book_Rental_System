using Catalog.Domain.IRepositories;
using Catalog.Infrastructure.Data;
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
    private readonly CatalogDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISnowFlakeService _snowFlakeService;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork, IIntegrationEventPublisher eventPublisher, ISnowFlakeService snowFlakeService, CatalogDbContext dbContext)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _snowFlakeService = snowFlakeService;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken ct)
    {
        await using var transaction = await  _dbContext.Database.BeginTransactionAsync(ct);
       
        try
        {
            _bookRepository.DeleteBook(request.BookId, ct);
            var bookDeletedEvent = new BookDeletedIntegrationEvent
            {
                BookId = request.BookId,
                CorrelationId = Guid.NewGuid(),
            };
            await _eventPublisher.Publish<BookDeletedIntegrationEvent>(bookDeletedEvent, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch (Exception)
        {

            await transaction.RollbackAsync(ct);
        }
        return Unit.Value;
    }
}
