using Notification.Domain.IRepositories;
using Notification.Infrastructure.Data;

namespace Notification.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitofWork
{
    private readonly NotificationDbContext _context;

    public UnitOfWork(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }
}
