using Framework.CQRS;

namespace Rental.ApplicationServices.Commands;

public record AddRentalBookCommand(long BookId, long UserId, DateTime BorrowDate): ICommand;
