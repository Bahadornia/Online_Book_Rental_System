using Framework.CQRS;

namespace Catalog.ApplicationServices.Commands;

public record DeleteBookCommand(long BookId): ICommand;
