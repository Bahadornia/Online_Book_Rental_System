using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Data;

namespace Notification.Infrastructure;

public static class NotificationInfrastructureBootstrapper
{
    public static IServiceCollection AddNotificationInfratructureServices(this IServiceCollection services)
    {
        services.AddScoped<NotificationDbContext>();
        return services;
    }
}
