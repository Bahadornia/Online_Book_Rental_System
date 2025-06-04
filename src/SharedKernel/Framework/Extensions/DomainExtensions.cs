using Framework.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Extensions;

public static class DomainExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
        return services;
    }
}
