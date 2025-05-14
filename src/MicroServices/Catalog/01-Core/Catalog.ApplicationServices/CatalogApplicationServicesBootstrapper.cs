using Microsoft.Extensions.DependencyInjection;
using Framework;
using Framework.CQRS;
using Minio;
using Microsoft.Extensions.Configuration;
namespace Catalog.ApplicationServices;

public static class CatalogApplicationServicesBootstrapper
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddMapsterService();
        return services;
    }
}
