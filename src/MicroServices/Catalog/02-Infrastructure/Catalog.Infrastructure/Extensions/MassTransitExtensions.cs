using Catalog.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Extensions;

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
       where T : DbContext
    {
        var rabbitConfig = configuration.GetSection("RabbitMq");
        services.AddMassTransit(x =>
         {
             x.SetKebabCaseEndpointNameFormatter();
             x.AddEntityFrameworkOutbox<T>(o =>
             {
                 // configure which database lock provider to use (Postgres, SqlServer, or MySql)
                 o.UseSqlServer();

                 // enable the bus outbox
                 o.UseBusOutbox();
             });


             x.AddConfigureEndpointsCallback((context, name, cfg) => { cfg.UseEntityFrameworkOutbox<CatalogDbContext>(context); });
         });

        return services;
    }
}
