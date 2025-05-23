using Framework.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Rental.ApplicationServices;

public static class RentalApplicatioServiceBootstrapper
{
    public static IServiceCollection AddRentalApplicationServies(this IServiceCollection services)
    {
        services.AddMediatRServices();
        services.AddMapsterService();
        services.AddSnowflakeService();
        return services;
    }
}
