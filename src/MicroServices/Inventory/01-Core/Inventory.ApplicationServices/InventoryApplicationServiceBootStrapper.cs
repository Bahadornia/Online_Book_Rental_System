using Inventory.Domain.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.ApplicationServices;

public static class InventoryApplicationServiceBootStrapper
{
    public static IServiceCollection AddInventoryApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}
