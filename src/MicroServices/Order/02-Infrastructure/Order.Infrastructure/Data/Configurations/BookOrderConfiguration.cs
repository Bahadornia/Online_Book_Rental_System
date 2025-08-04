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
        builder.HasKey(o => o.Id);
        builder.Property(o => o.DueDate)
            .HasComputedColumnSql("DATEADD(DAY, 14, [BorrowDate])", stored: true);
        builder.HasIndex(o => o.DueDate)
            .IncludeProperties(o => new { o.BookId, o.UserId });
          
        builder.Property(o => o.Id).HasConversion(o => o.Value, o => OrderId.Create(o));
        builder.Property(o => o.BookId).HasConversion(b => b.Value, b => BookId.Create(b));
        builder.Property(o => o.UserId).HasConversion(u => u.Value, u => UserId.Create(u));
        builder.Property(o => o.Status).HasConversion(b => b.ToString(), b => Enum.Parse<OrderStatus>(b));
        builder.HasMany(o => o.Histories)
            .WithOne(oh => oh.Order).HasForeignKey(oh => oh.OrderId);
    }
}
