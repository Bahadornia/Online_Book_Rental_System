using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental.Domain.Models.RentalAggregate.Entities;
using Rental.Domain.Models.RentalAggregate.Enums;
using Rental.Domain.Models.RentalAggregate.ValueObjects;

namespace Rental.Infrastructure.Data.Configurations;

public sealed class BookRentalConfiguration : IEntityTypeConfiguration<BookRental>
{
    public void Configure(EntityTypeBuilder<BookRental> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasConversion(b => b.Value, b => RentalId.Create(b));
        builder.Property(b => b.BookId).HasConversion(b => b.Value, b => BookId.Create(b));
        builder.Property(b => b.UserId).HasConversion(b => b.Value, b => UserId.Create(b));
        builder.Property(b => b.Status).HasConversion(b => b.ToString(), b => Enum.Parse<RentalStatus>(b));
        builder.HasMany(b=> b.Histories)
            .WithOne(b => b.Rental).HasForeignKey(b => b.RentalId);
    }
}
