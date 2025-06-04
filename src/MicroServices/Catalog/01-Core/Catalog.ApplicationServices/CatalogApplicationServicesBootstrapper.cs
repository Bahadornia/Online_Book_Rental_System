using Framework.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.ApplicationServices;

public static class CatalogApplicationServicesBootstrapper
{
    public static IServiceCollection AddCatalogApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(CatalogApplicationServicesBootstrapper));
        services.AddMediatRServices(assembly);
        services.AddSnowflakeService();
        return services;
    }
}
