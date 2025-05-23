using Framework.CQRS;
using Framework.SnowFlake;
using MapsterMapper;
using MediatR;
using Rental.Domain.Dtos;
using Rental.Domain.IRepositories;
using Rental.Domain.IServices;
using Rental.Domain.Models.RentalAggregate.Entities;

namespace Rental.ApplicationServices.Commands;

class AddRentalBookCommandHandler : ICommandHandler<AddRentalBookCommand>
{
    private readonly ISnowFlakeService _snowFlakeService;
    private readonly IRentalRepository _rentalRepository;

    private readonly IMapper _mapper;

    public AddRentalBookCommandHandler(IRentalRepository rentalRepository, ISnowFlakeService snowFlakeService)
    {
        _rentalRepository = rentalRepository;
        _snowFlakeService = snowFlakeService;
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
        await _rentalRepository.AddBookRental(bookRental, ct);
        return Unit.Value;
    }
}
