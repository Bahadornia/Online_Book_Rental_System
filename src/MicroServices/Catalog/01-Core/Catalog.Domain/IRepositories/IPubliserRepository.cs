using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories
{
    public interface IPubliserRepository
    {
        Task Add(Publisher publisher, CancellationToken ct);
        Task<IReadOnlyCollection<Publisher>> GetAll(string publisher, CancellationToken ct);
    }
}
