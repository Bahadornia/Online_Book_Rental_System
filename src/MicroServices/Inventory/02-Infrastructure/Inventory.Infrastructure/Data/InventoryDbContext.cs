using Inventory.Domain.Models.InventoryAggregate.Entities;
using Inventory.Infrastructure.Extensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Inventory.Infrastructure.Data;

public sealed class InventoryDbContext: DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddMassTransitModels();
    }

    public DbSet<BookInventory> Inventories => Set<BookInventory>();

}
