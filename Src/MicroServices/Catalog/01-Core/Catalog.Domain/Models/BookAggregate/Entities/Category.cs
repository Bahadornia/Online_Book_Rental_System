using Core;

namespace Catalog.Domain.Model.BookAggregate.Entities
{
    class Category: Entity<Guid>
    {
        public string Name { get; set; } = default!;
    }
}
