using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.Entities;

public sealed class Publisher : Entity<int>
{
    public string Name { get; set; } = default!;
    public List<Book> Books { get; private set; } = [];

    public static Publisher Create(string Name)
    {
        var publisher = new Publisher
        {
            Name = Name
        };
        return publisher;
    }
}
