using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rental.Domain.IRepositories;
using Rental.Domain.IServices;
using Rental.Infastructure.Data;
using Rental.Infastructure.Repositories;
using Rental.Infastructure.Services;

namespace Rental.Infastructure;

public static class RentalInfastructureBootstrapper
{
    public static IServiceCollection AddRentalInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<RentalDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefatultDatabase"));
        });
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IRentalService, RentalService>();
        return services;

    }
}
