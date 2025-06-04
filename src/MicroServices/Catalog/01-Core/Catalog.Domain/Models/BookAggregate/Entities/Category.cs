using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.Entities;

class Category: Entity<Guid>
{
    public string Name { get; set; } = default!;
}
