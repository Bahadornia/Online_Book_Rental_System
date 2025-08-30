namespace Notification.Domain.Models.Entities;

public class OutboxMessage
{
    public long Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Payload { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }

}
