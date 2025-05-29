using Framework.CQRS;

namespace Inventory.ApplicationServices.Commands;

public record DecreaseAvailableCopiesCommand(long BookId):ICommand;
