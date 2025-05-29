using Inventory.Domain.IRepositories;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories;

internal class UnitofWork : IUnitofWork
{
    private readonly InventoryDbContext _dbContext;

    public UnitofWork(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _dbContext.SaveChangesAsync(ct);
    }
}
