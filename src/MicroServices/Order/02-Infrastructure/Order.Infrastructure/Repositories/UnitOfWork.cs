using Order.Domain.IRepositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly OrderDbContext _dbContext;

    public UnitOfWork(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BenginTransacttionAsync(CancellationToken ct)
    {
       await _dbContext.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransacttionAsync(CancellationToken ct)
    {
        await _dbContext.Database.CommitTransactionAsync(ct);
    }

    public async Task RollBackTransacttionAsync(CancellationToken ct)
    {
        await _dbContext.Database.RollbackTransactionAsync(ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}
