using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Enums;
using Notification.Domain.Models.Entities;

namespace Notification.Infrastructure.Data.Configurations;

internal class NotificatoinConfiguration : IEntityTypeConfiguration<NotificationEntity>
{
    public void Configure(EntityTypeBuilder<NotificationEntity> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Priority).HasConversion(n => n.ToString(), n => Enum.Parse<Priority>(n));
        builder.Property(n => n.Type).HasConversion(n => n.ToString(), n => Enum.Parse<EventType>(n));
        builder.Property(n => n.Status).HasConversion(n => n.ToString(), n => Enum.Parse<NotificationStatus>(n));
    }
}
