using Framework.CQRS;
using Framework.SnowFlake;
using MediatR;
using Rental.Domain.Dtos;
using Rental.Domain.IRepositories;
using Rental.Domain.IServices;
using Rental.Domain.Models.RentalAggregate.Entities;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;

namespace Rental.ApplicationServices.Commands;

class AddRentalBookCommandHandler : ICommandHandler<AddRentalBookCommand>
{
    private readonly ISnowFlakeService _snowFlakeService;
    private readonly IRentalRepository _rentalRepository;
    private readonly IRentalService _rentalService;
    private readonly IIntegrationEventPublisher _eventPublisher;

    public AddRentalBookCommandHandler(IRentalRepository rentalRepository, ISnowFlakeService snowFlakeService, IIntegrationEventPublisher publishEndPoint, IRentalService rentalService)
    {
        _rentalRepository = rentalRepository;
        _snowFlakeService = snowFlakeService;
        _eventPublisher = publishEndPoint;
        _rentalService = rentalService;
    }

    public async Task<Unit> Handle(AddRentalBookCommand command, CancellationToken ct)
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
        var dto = new BookRentalDto(id, command.UserId, command.BookId, command.BorrowDate);
        var bookRental = BookRental.Create(dto.Id, dto.UserId, dto.BookId, dto.BorrowDate);
        var history = new RentalHistory
        {
            Id = _snowFlakeService.CreateId(),
            RentalId = bookRental.Id,
            Status = bookRental.Status,
            Description = bookRental.Description
        };

        bookRental.AddRentalHistory(history);
        var bookRentedEvent = new BookRentedIntegrationEvent
        {
            EventId = _snowFlakeService.CreateId(),
            BookId = command.BookId,
            BorrowDate = command.BorrowDate,
        };

        await _eventPublisher.Publish<BookRentedIntegrationEvent>(bookRentedEvent, ct);
        await _rentalRepository.AddBookRental(bookRental, ct);
        return Unit.Value;
    }

    private async Task<bool> CanUserRentBook(long userId, CancellationToken ct)
    {
        return await Task.FromResult(true);
    }

    private async Task<bool> IsBookAvailalbe(long bookId, CancellationToken ct)
    {
        var isBookAvailable = await _rentalService.IsBookAvailable(bookId, ct);
        return isBookAvailable;
    }
}
