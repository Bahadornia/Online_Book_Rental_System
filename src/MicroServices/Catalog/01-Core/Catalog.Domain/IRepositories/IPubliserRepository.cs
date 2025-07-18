namespace Catalog.Domain.IRepositories
{
    public interface IPubliserRepository
    {
        Task AddPubliser(string publisher, CancellationToken ct);
        Task<IReadOnlyCollection<string>> GetAll(string publisher, CancellationToken ct);
    }
}
