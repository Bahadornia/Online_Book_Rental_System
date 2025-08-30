using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Notification.Domain.IRepositories;
using Notification.Infrastructure.Data;
using Notification.Infrastructure.Hubs;
using Notification.Infrastructure.Repositories;
using Notification.Infrastructure.Services;
using SharedKernel.Messaging.Extensions;

namespace Notification.Infrastructure;

public static class NotificationInfrastructureBootstrapper
{
    public static IServiceCollection AddNotificationInfratructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddHostedService<OutboxProcessorJob>();
        services.AddScoped<NotificationDbContext>();
        services.AddScoped<IUnitofWork, UnitOfWork>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IOutboxMessgeRepository, OutboxMessgeRepository>();
        services.AddSignalR();
        services.AddMessagingServices();
        services.AddCors((corsOptions) =>
        {
            corsOptions.AddPolicy("ClientHub", (p) =>
            {
                p.WithOrigins("https://localhost:7229")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
               
            });
        });
        services.AddSingleton<IUserIdProvider, SubUserIdProvider>();
        services.AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetConnectionString("Database")));
        services.AddSingleton(provider => provider.GetRequiredService<IMongoClient>().GetDatabase("NotificationDb"));
        return services;
    }

    public static IApplicationBuilder UseNotificationServices(this IApplicationBuilder app)
    { 
        app.UseCors("ClientHub");
        return app;
    }
    public static IEndpointRouteBuilder UseHubRoutes(this IEndpointRouteBuilder endPoints, string route)
    {
        endPoints.MapHub<OrdersNotificationHub>(route).RequireAuthorization();
        return endPoints;
    }

    public static IApplicationBuilder InitializeNotifcationDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        dbContext.InitializeMongoDb().GetAwaiter().GetResult(); 
      return app;
    }
}
