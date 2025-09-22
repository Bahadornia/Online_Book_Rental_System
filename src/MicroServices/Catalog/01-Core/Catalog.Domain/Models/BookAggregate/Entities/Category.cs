using Catalog.Domain.Models.BookAggregate.ValueObjects;
using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.Entities;

public sealed class Category: Entity<CategoryId>
{
    public string Name { get; set; } = default!;
    public List<Book> Books { get; set; }= default!;

    private Category() { }

    public static Category Create(long id, string name)
    {
        return new Category
        {
            Id = id,
            Name = name,
        };
    }

}
