using Catalog.Domain.Models.BookAggregate.ValueObjects;
using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.Entities;

public sealed class Publisher: Entity<PublisherId>
{
    public string Name { get; set; } = default!;
    public List<Book> Books { get; set; } = [];

    private Publisher() { }

    public static Publisher Create(string Name)
    {
        var publisher = new Publisher
        {
            Name = Name
        };
        return publisher;
    }
}
