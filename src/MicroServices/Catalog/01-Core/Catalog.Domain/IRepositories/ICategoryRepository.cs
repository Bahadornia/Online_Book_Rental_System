using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories
{
    public interface ICategoryRepository
    {
        Task Add(CategoryDto publisher, CancellationToken ct);
        Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken ct);
    }
}
