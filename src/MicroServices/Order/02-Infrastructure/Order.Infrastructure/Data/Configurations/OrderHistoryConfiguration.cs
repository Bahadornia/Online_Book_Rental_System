using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Models.RentalAggregate.Entities;
using Order.Domain.Models.RentalAggregate.Enums;
using Order.Domain.Models.RentalAggregate.ValueObjects;

namespace Order.Infrastructure.Data.Configurations;

public sealed class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(b => b.Id).HasConversion(b => b.Value, b=> OrderHistroyId.Create(b));
        builder.Property(b => b.OrderId).HasConversion(b => b.Value, b=> OrderId.Create(b));
        builder.Property(b => b.Status).HasConversion(b => b.ToString(), b=> Enum.Parse<OrderStatus>(b));
        builder.Property(b => b.Description).IsSparse();
        builder.HasOne(b=> b.Rental).WithMany(b => b.Histories);
    }
}
