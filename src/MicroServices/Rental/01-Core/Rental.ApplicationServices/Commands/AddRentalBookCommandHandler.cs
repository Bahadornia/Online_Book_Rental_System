using Framework.CQRS;
using Framework.SnowFlake;
using MapsterMapper;
using MassTransit;
using MediatR;
using Rental.Domain.Dtos;
using Rental.Domain.IRepositories;
using Rental.Domain.IServices;
using Rental.Domain.Models.RentalAggregate.Entities;
using SharedKernel.Messaging.Events;

namespace Rental.ApplicationServices.Commands;

class AddRentalBookCommandHandler : ICommandHandler<AddRentalBookCommand>
{
    private readonly ISnowFlakeService _snowFlakeService;
    private readonly IRentalRepository _rentalRepository;
    private readonly IPublishEndpoint _publishEndPoint;

    public AddRentalBookCommandHandler(IRentalRepository rentalRepository, ISnowFlakeService snowFlakeService, IPublishEndpoint publishEndPoint)
    {
        _rentalRepository = rentalRepository;
        _snowFlakeService = snowFlakeService;
        _publishEndPoint = publishEndPoint;
    }

    public async Task<Unit> Handle(AddRentalBookCommand command, CancellationToken ct)
    {
        var id = _snowFlakeService.CreateId();
        var dto = new BookRentalDto(id, command.UserId, command.BookId, command.BorrowDate);
        var bookRental = BookRental.Create(dto.Id, dto.UserId, dto.BookId, dto.BorrowDate);
        var history = new RentalHistory
        {
            Id = _snowFlakeService.CreateId(),
            RentalId = bookRental.Id,
            Status= bookRental.Status,
            Description = bookRental.Description
        };

        bookRental.AddRentalHistory(history);
        var bookRentedEvent = new BookRented
        {
            EventId = _snowFlakeService.CreateId(),
            BookId = command.BookId,
            BorrowDate = command.BorrowDate,
        };

        await _publishEndPoint.Publish(bookRentedEvent, ct);
        await _rentalRepository.AddBookRental(bookRental, ct);
        return Unit.Value;
    }
}
