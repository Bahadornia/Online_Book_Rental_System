namespace Order.Domain.IRepositories;

public interface IUnitOfWork
{
    Task BenginTransacttionAsync(CancellationToken ct);
    Task RollBackTransacttionAsync(CancellationToken ct);
    Task CommitTransacttionAsync(CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
