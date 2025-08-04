using Notification.Domain.IRepositories;
using Notification.Domain.Models.Entities;
using Notification.Infrastructure.Data;

namespace Notification.Infrastructure.Repositories;

public sealed class NotificationRepository : INotificationRepository
{
    private readonly NotificationDbContext _dbContext;

    public NotificationRepository(NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(NotificationEntity notification, CancellationToken ct)
    {
        await _dbContext.Notifications.InsertOneAsync(_dbContext.Session,notification, null, ct);
    }
}
