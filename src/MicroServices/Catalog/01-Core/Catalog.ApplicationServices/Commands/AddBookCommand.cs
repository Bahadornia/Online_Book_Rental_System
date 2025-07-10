using Framework.CQRS;

namespace Catalog.ApplicationServices.Commands;

public record AddBookCommand(
    string Title,
    string Author,
    string Publisher,
    string Category,
    string ISBN,
    string? Description,
    string ContentType,
    int AvailableCopies,
    byte[] Image) : ICommand
{
}
