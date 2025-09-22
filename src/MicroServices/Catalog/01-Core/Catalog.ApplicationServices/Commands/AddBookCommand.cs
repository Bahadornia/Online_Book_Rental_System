using Framework.CQRS;

namespace Catalog.ApplicationServices.Commands;

public record AddBookCommand(
    string Title,
    string Author,
    string PublisherName,
    string CategoryName,
    string ISBN,
    string? Description,
    string ContentType,
    int AvailableCopies,
    byte[] Image) : ICommand
{
}
