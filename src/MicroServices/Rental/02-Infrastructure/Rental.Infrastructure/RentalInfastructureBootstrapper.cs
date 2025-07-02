using Framework.Interceptors;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Rental.Domain.IRepositories;
using Rental.Domain.IServices;
using Rental.Infrastructure.Data;
using Rental.Infrastructure.Extensions;
using Rental.Infrastructure.Repositories;
using Rental.Infrastructure.Services;
using Rental.Infrastructure.Services.Refit;

namespace Rental.Infrastructure;

public static class RentalInfastructureBootstrapper
{
    public static IServiceCollection AddRentalInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransitService<RentalDbContext>(configuration);
        services.AddSingleton<PublishDomainEventInterceptor>();
        services.AddDbContextPool<RentalDbContext>((sp, opt) =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefatultDatabase"));
            opt.AddInterceptors(sp.GetRequiredService<PublishDomainEventInterceptor>());
        });
        services.AddRefitClient<IInventoryApi>().ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://localhost:7010/api/Inventory");
        });
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IRentalService, RentalService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;

    }
}
