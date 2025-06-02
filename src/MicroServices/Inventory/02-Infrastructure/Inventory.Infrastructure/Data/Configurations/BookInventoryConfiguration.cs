using Inventory.Domain.Models.InventoryAggregate.Entities;
using Inventory.Domain.Models.InventoryAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Data.Configurations;

class BookInventoryConfiguration : IEntityTypeConfiguration<BookInventory>
{
    public void Configure(EntityTypeBuilder<BookInventory> builder)
    {
        builder.HasKey(bi => bi.Id);
        builder.Property(bi => bi.Id).HasConversion(bi => bi.Value, bi => BookId.Create(bi));
    }
}
