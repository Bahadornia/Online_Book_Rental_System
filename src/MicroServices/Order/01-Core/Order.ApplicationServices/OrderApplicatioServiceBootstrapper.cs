using Framework.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.ApplicationServices;

public static class RentalApplicatioServiceBootstrapper
{
    public static IServiceCollection AddRentalApplicationServies(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(RentalApplicatioServiceBootstrapper));
        services.AddMediatRServices(assembly);
        services.AddMapsterService(Assembly.GetExecutingAssembly());
        services.AddSnowflakeService();
        return services;
    }
}
