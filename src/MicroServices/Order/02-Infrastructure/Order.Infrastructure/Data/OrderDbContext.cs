using Microsoft.EntityFrameworkCore;
using Order.Domain.Models.OrderAggregate.Entities;
using Order.Infrastructure.Extensions;
using System.Reflection;

namespace Order.Infrastructure.Data;

public sealed class OrderDbContext: DbContext
{
    public DbSet<BookOrder> BookOrders => Set<BookOrder>();
    public DbSet<OrderHistory> OrderHistories => Set<OrderHistory>();
    public OrderDbContext(DbContextOptions<OrderDbContext> options): base(options) { }

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
