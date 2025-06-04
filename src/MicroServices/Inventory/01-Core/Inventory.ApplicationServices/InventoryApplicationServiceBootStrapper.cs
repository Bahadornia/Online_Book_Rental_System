using Framework.Extensions;
using Inventory.Domain.IServices;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.ApplicationServices;

public static class InventoryApplicationServiceBootStrapper
{
    public static IServiceCollection AddInventoryApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(InventoryApplicationServiceBootStrapper));
        services.AddMediatRServices(assembly);
        services.AddSnowflakeService();
        return services;
    }
}
