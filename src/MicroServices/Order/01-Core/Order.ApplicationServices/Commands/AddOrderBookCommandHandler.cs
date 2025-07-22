using Framework.CQRS;
using Framework.SnowFlake;
using MediatR;
using Order.Domain.Dtos;
using Order.Domain.IRepositories;
using Order.Domain.IServices;
using Order.Domain.Models.OrderAggregate.Entities;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;

namespace Order.ApplicationServices.Commands;

class AddOrderBookCommandHandler : ICommandHandler<AddOrderBookCommand>
{
    private readonly ISnowFlakeService _snowFlakeService;
    private readonly IOrderRepository _OrderRepository;
    private readonly IOrderService _OrderService;
    private readonly IIntegrationEventPublisher _eventPublisher;
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderBookCommandHandler(IOrderRepository OrderRepository, ISnowFlakeService snowFlakeService, IIntegrationEventPublisher publishEndPoint, IOrderService OrderService, IUnitOfWork unitOfWork)
    {
        _OrderRepository = OrderRepository;
        _snowFlakeService = snowFlakeService;
        _eventPublisher = publishEndPoint;
        _OrderService = OrderService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddOrderBookCommand command, CancellationToken ct)
    {

        if (!await IsBookAvailalbe(command.BookId, ct))
        {
            throw new Exception("Book is not avialable");
        }
        if (!await CanUserRentBook(command.UserId, ct))
        {
            throw new Exception("You can not rent a book!");
        }

        var id = _snowFlakeService.CreateId();
        var dto = new AddBookOrderDto(id, command.UserId, command.BookId, command.BorrowDate);
        var bookOrder = BookOrder.Create(dto.Id, dto.UserId, dto.BookId, dto.BorrowDate);
        var history = new OrderHistory
        {
            Id = _snowFlakeService.CreateId(),
            OrderId = bookOrder.Id,
            Status = bookOrder.Status,
            Description = bookOrder.Description
        };

        bookOrder.AddOrderHistory(history);
        var bookRentedEvent = new BookRentedIntegrationEvent
        {
            EventId = _snowFlakeService.CreateId(),
            BookId = command.BookId,
            BorrowDate = command.BorrowDate,
        };

        await _unitOfWork.BenginTransacttionAsync(ct);
        await _eventPublisher.Publish<BookRentedIntegrationEvent>(bookRentedEvent, ct);
        _OrderRepository.AddBookOrder(bookOrder, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        try
        {
            await _unitOfWork.CommitTransacttionAsync(ct);
        }
        catch (Exception)
        {

            await _unitOfWork.RollBackTransacttionAsync(ct);
            throw new Exception("Error in renting the book!");
        }
        return Unit.Value;
    }

    private async Task<bool> CanUserRentBook(long userId, CancellationToken ct)
    {
        return await Task.FromResult(true);
    }

    private async Task<bool> IsBookAvailalbe(long bookId, CancellationToken ct)
    {
        var isBookAvailable = await _OrderService.IsBookAvailable(bookId, ct);
        return isBookAvailable;
    }
}
