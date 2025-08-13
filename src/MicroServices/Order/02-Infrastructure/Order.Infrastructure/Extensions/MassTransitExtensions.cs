using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Sagas;
using Order.Infrastructure.Sagas.BookOrderSagas;
using System.Reflection;

namespace Order.Infrastructure.Extensions;

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
             x.AddSagaStateMachine<BookOrderSaga, BookOrderSagaState>()
       .EntityFrameworkRepository(r =>
       {
           r.ConcurrencyMode = ConcurrencyMode.Optimistic;
           r.AddDbContext<DbContext, OrderSagaDbContext>((provider, builder) =>
           {
               builder.UseSqlServer(configuration.GetConnectionString("DefatultDatabase"), m =>
               {
                   m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                   m.MigrationsHistoryTable($"__{nameof(OrderSagaDbContext)}");
               });
           });
       });
             x.AddEntityFrameworkOutbox<T>(o =>
             {
                 o.UseSqlServer();
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
