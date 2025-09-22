using Framework.CQRS;

namespace Catalog.ApplicationServices.Commands;

public record AddBookCommand(
    string Title,
    string Author,
    string PublisherName,
    int PublisherId,
    string CategoryName,
    int CategoryId,
    string ISBN,
    string? Description,
    string ContentType,
    int AvailableCopies,
    byte[] Image) : ICommand
{
}
