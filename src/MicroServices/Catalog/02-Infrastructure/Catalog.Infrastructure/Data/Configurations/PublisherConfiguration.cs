using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Domain.Models.BookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations;

internal sealed class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(p => p.Value, id => PublisherId.Create(id)).UseIdentityColumn();
        builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
        builder.HasMany(p => p.Books).WithOne(b => b.Publiser);
    }
}
