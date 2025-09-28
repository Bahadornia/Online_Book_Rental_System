using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.Entities;

public sealed class Category : Entity<int>
{
    public string Name { get; set; } = default!;
    public List<Book> Books { get; set; } = [];
}
