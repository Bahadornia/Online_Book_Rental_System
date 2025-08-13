using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Domain.IRepositories
{
    public interface ICategoryRepository
    {
        Task Add(Category publisher, CancellationToken ct);
        Task<IReadOnlyCollection<Category>> GetAll(string publisher, CancellationToken ct);
    }
}
