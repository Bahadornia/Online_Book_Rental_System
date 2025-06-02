using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental.Domain.Models.RentalAggregate.Entities;
using Rental.Domain.Models.RentalAggregate.Enums;
using Rental.Domain.Models.RentalAggregate.ValueObjects;

namespace Rental.Infrastructure.Data.Configurations;

public sealed class RentalHistoryConfiguration : IEntityTypeConfiguration<RentalHistory>
{
    public void Configure(EntityTypeBuilder<RentalHistory> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(b => b.Id).HasConversion(b => b.Value, b=> RentalHistroyId.Create(b));
        builder.Property(b => b.RentalId).HasConversion(b => b.Value, b=> RentalId.Create(b));
        builder.Property(b => b.Status).HasConversion(b => b.ToString(), b=> Enum.Parse<RentalStatus>(b));
        builder.Property(b => b.Description).IsSparse();
        builder.HasOne(b=> b.Rental).WithMany(b => b.Histories);
    }
}
