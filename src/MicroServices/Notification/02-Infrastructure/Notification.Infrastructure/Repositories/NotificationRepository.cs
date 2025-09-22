using Notification.Domain.IRepositories;
using Notification.Domain.Models.Entities;
using Notification.Infrastructure.Data;

namespace Notification.Infrastructure.Repositories;

public sealed class NotificationRepository : INotificationRepository
{
    private readonly IUnitofWork _unitofWork;
    private readonly NotificationDbContext _dbContext;

    public NotificationRepository(NotificationDbContext dbContext, IUnitofWork unitofWork)
    {
        _dbContext = dbContext;
        _unitofWork = unitofWork;
    }

    public async Task Add(NotificationEntity notification, CancellationToken ct)
    {
        _dbContext.Notifications.Add(notification);
        await _unitofWork.SaveChangesAsync(ct);
    }
}
