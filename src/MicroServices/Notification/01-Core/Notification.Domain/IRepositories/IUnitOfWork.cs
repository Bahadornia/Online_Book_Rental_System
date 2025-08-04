namespace Notification.Domain.IRepositories;

public interface IUnitofWork
{
    Task BeginTransaction(CancellationToken ct = default);
    Task CommitTransaction(CancellationToken ct = default);
    Task AbortTransaction(CancellationToken ct = default);
}
