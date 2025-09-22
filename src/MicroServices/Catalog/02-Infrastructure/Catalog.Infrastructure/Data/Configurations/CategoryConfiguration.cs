using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Domain.Models.BookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasAlternateKey(c => c.Name);
        builder.Property(c=> c.Id).HasConversion(c => c.Value, c => CategoryId.Create(c)).UseIdentityColumn();
        builder.Property(c => c.Name).HasMaxLength(150).IsRequired();
        builder.HasMany(c => c.Books).WithOne(b => b.Category);
    }
}
