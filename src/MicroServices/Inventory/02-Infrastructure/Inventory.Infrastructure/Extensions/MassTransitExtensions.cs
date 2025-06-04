using Inventory.Infrastructure.Consumers;
using Inventory.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Messaging.Events;

namespace Inventory.Infrastructure.Extensions;

public static class MassTransitExtensions
{
    public static ModelBuilder AddMassTransitModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        return modelBuilder;
    }
    public static IServiceCollection AddMassTransitServices<T>(this IServiceCollection services, IConfiguration configuration)
        where T:DbContext
    {
        var rabbitConfig = configuration.GetSection("RabbitMq");
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BookRentedConsumer>();
            x.AddConsumer<BookAddedConsumer>();
            x.AddConsumer<BookAddedFualtConsumer>();

            x.SetKebabCaseEndpointNameFormatter();
            x.AddEntityFrameworkOutbox<T>(o =>
            {
                // configure which database lock provider to use (Postgres, SqlServer, or MySql)
                o.UseSqlServer();

                // enable the bus outbox
                o.UseBusOutbox();
            });


            x.AddConfigureEndpointsCallback((context, name, cfg) => { cfg.UseEntityFrameworkOutbox<InventoryDbContext>(context); });


            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitConfig["Host"], h =>
                {
                    h.Username(rabbitConfig["Username"]!);
                    h.Password(rabbitConfig["Password"]!);
                });


                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
