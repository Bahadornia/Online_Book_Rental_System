using Microsoft.EntityFrameworkCore;
using Notification.Domain.Models.Entities;
using System.Reflection;

namespace Notification.Infrastructure.Data;

public sealed class NotificationDbContext: DbContext
{
   

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options):base(options)
    {
       
    }


    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<NotificationEntity> Notifications => Set<NotificationEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
    }

    public async Task InitializeMongoDb()
    {
        
    }
}
