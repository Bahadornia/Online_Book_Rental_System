using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Models.Entities;

namespace Notification.Infrastructure.Configurations
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(n => n.Id);
            builder.HasIndex(n => n.ProcessedAt);

        }
    }
}
