using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Consumers;

namespace Notification.Infrastructure.Extensions;

public static class MassTransitExtensions
{
    public static ModelBuilder AddMassTransitModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        return modelBuilder;
    }

    public static IServiceCollection AddMassTransitService<T>(this IServiceCollection services, IConfiguration configuration)
        where T:DbContext
    {
        var rabbitConfig = configuration.GetSection("RabbitMq");
        services.AddMassTransit(x =>
         {
             x.AddConsumer<BookAddedConsumer>();
             x.AddConsumer<OrdersOverDueDatedConsumer>();
             x.SetKebabCaseEndpointNameFormatter();
             x.AddEntityFrameworkOutbox<T>(o =>
             {
                 // configure which database lock provider to use (Postgres, SqlServer, or MySql)
                 o.UseSqlServer();

                 // enable the bus outbox
                 o.UseBusOutbox();
             });
             x.UsingRabbitMq((context, cfg) =>
             {
                 cfg.Host(rabbitConfig["Host"], h =>
                 {
                     h.Username(rabbitConfig["Username"]!);
                     h.Password(rabbitConfig["Password"]!);
                 });


                 cfg.ConfigureEndpoints(context);
                 cfg.ReceiveEndpoint("notification-book-added-queue", e =>
                 {
                     e.ConfigureConsumer<BookAddedConsumer>(context);
                 });
             });
         });
        return services;
    }
}
