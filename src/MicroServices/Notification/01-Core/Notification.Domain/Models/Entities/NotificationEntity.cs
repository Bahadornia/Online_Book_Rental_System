using Notification.Domain.Enums;

namespace Notification.Domain.Models.Entities;

public class NotificationEntity
{
    public long Id { get; set; }
    public DateTime CretedAt { get; set; }
    public EventType Type { get; set; }
    public string Message { get; set; } = default!;
    public Priority Priority { get; set; }
    public NotificationStatus Status { get; set; }
}
