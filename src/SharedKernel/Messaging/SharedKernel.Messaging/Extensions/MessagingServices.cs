using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Messaging.Extensions;

public static class MessagingServices
{
    public static IServiceCollection AddMessagingServices(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventPublisher, IntegrationEventPulbisher>();
        return services;
    }
}
