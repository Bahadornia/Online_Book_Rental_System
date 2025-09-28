using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories
{
    public interface ICategoryRepository
    {
        void Add(CategoryDto publisher);
        Task<IReadOnlyCollection<Category>> GetAll(string term, CancellationToken ct);
    }
}
