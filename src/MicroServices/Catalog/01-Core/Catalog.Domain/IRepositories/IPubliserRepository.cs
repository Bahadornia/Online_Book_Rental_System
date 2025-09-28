using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories
{
    public interface IPubliserRepository
    {
        void Add(PublisherDto publisher);
        Task<IReadOnlyCollection<Publisher>> GetAll(string term, CancellationToken ct);
    }
}
