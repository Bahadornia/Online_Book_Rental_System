using Notification.Domain.Models.Entities;

namespace Notification.Domain.IRepositories;

public interface INotificationRepository
{
    Task Add(NotificationEntity notification, CancellationToken ct = default);
}
