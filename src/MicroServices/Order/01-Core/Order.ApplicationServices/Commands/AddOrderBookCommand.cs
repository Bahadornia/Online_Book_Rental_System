using Framework.CQRS;

namespace Order.ApplicationServices.Commands;

public record AddOrderBookCommand(long BookId, string UserId, DateTime BorrowDate): ICommand;
