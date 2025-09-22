using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Domain.Models.BookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Configurations;

internal class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasConversion(b => b.Value, b => BookId.Create(b));
        builder.Property(b => b.ISBN).HasConversion(b => b.Value, b => ISBN.Create(b));
        builder.HasOne(b => b.Category).WithMany(c => c.Books);
        builder.HasOne(b=> b.Publiser).WithMany(p => p.Books);

    }
}
