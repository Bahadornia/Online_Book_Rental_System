using Framework.CQRS;

namespace Catalog.ApplicationServices.Commands;

public record AddBookCommand(
    string Title,
    string Author,
    int PublisherId,
    int CategoryId,
    long ISBN,
    string? Description,
    string ContentType,
    int AvailableCopies,
    byte[] Image) : ICommand
{
}
