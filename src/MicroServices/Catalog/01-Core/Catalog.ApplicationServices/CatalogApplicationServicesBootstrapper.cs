using Microsoft.Extensions.DependencyInjection;
using Framework;
namespace Catalog.ApplicationServices;

public static class CatalogApplicationServicesBootstrapper
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddMapsterService();
        return services;
    }
}
