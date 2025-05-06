namespace Catalog.Domain.Model.BookAggregate.Entities;

class Publisher<Guid>
{
    public string Name { get; set; } = default!;
    public string Country { get; set; } = default!;
}
