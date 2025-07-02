using Rental.Domain.IRepositories;
using Rental.Infrastructure.Data;

namespace Rental.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly RentalDbContext _dbContext;

    public UnitOfWork(RentalDbContext dbContext)
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
