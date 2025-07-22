using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Enums;
using Order.Domain.Models.OrderAggregate.Entities;
using Order.Domain.Models.OrderAggregate.ValueObjects;

namespace Order.Infrastructure.Data.Configurations;

public sealed class BookRentalConfiguration : IEntityTypeConfiguration<BookOrder>
{
    public void Configure(EntityTypeBuilder<BookOrder> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasConversion(b => b.Value, b => OrderId.Create(b));
        builder.Property(b => b.BookId).HasConversion(b => b.Value, b => BookId.Create(b));
        builder.Property(b => b.UserId).HasConversion(b => b.Value, b => UserId.Create(b));
        builder.Property(b => b.Status).HasConversion(b => b.ToString(), b => Enum.Parse<OrderStatus>(b));
        builder.HasMany(b => b.Histories)
            .WithOne(b => b.Order).HasForeignKey(b => b.OrderId);
    }
}
