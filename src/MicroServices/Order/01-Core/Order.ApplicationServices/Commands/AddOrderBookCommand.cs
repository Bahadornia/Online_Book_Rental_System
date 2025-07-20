using Framework.CQRS;

namespace Order.ApplicationServices.Commands;

public record AddOrderBookCommand(long BookId, long UserId, DateTime BorrowDate): ICommand;
