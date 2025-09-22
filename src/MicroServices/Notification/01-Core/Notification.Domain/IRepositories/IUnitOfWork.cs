namespace Notification.Domain.IRepositories;

public interface IUnitofWork
{
   Task<int> SaveChangesAsync(CancellationToken ct = default!);
}
