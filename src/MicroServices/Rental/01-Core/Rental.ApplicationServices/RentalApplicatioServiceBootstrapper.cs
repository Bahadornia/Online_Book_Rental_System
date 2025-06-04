using Framework.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Rental.ApplicationServices;

public static class RentalApplicatioServiceBootstrapper
{
    public static IServiceCollection AddRentalApplicationServies(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(RentalApplicatioServiceBootstrapper));
        services.AddMediatRServices(assembly);
        services.AddMapsterService();
        services.AddSnowflakeService();
        return services;
    }
}
