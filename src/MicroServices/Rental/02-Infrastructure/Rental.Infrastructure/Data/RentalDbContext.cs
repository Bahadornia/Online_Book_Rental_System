using Microsoft.EntityFrameworkCore;
using Rental.Domain.Models.RentalAggregate.Entities;
using SharedKernel.Messaging.Extensions;
using System.Reflection;

namespace Rental.Infrastructure.Data;

public sealed class RentalDbContext: DbContext
{
    public DbSet<BookRental> BookRentals => Set<BookRental>();
    public DbSet<RentalHistory> RentalHistories => Set<RentalHistory>();
    public RentalDbContext(DbContextOptions<RentalDbContext> options): base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
       
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddMassTransitModels();
    }

}
