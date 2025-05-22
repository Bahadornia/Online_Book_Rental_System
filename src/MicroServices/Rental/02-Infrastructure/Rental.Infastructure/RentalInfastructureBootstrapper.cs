using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rental.Infastructure.Data;

namespace Rental.Infastructure;

public static class RentalInfastructureBootstrapper
{
    public static IServiceCollection AddRentalInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<RentalDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefatultDatabase"));
        });
        return services;

    }
}
