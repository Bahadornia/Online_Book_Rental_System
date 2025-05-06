namespace Library.Repository.Domain.Models.BookAggregate.Entities;

class Publisher<Guid>
{
    public string Name { get; set; } = default!;
    public string Country { get; set; } = default!;
}
