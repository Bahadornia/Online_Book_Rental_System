using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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

    public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration configuration)

    {
        var rabbitConfig = configuration.GetSection("RabbitMq");
        services.AddMassTransit(x =>
         {
             x.SetKebabCaseEndpointNameFormatter();
             x.AddMongoDbOutbox(o =>
             {
                 o.QueryDelay = TimeSpan.FromSeconds(1);

                 o.ClientFactory(provider => provider.GetRequiredService<IMongoClient>());
                 o.DatabaseFactory(provider => provider.GetRequiredService<IMongoDatabase>());

                 o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);

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
             });
         });
        return services;
    }
}
