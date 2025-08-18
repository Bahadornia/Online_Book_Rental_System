using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories
{
    public interface IPubliserRepository
    {
        Task Add(PublisherDto publisher, CancellationToken ct);
        Task<IReadOnlyCollection<PublisherDto>> GetAll(CancellationToken ct);
    }
}
