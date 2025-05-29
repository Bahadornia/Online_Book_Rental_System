namespace Inventory.Domain.IRepositories;

public interface IUnitofWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct);
}
