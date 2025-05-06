using Catalog.Domain.Model.Shared.Enums;
using Core;

namespace Catalog.Domain.Model.BookAggregate.Entities
{
    public class Book : AggregateRoot<Guid>
    {
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public int PulbisherId { get; set; }
        public int CategoryId { get; set; }
        public long ISBN { get; set; } = default!;
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
