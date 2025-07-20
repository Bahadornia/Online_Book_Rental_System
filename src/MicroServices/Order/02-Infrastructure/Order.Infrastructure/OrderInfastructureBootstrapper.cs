using Framework.Interceptors;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Order.Domain.IRepositories;
using Order.Domain.IServices;
using Order.Infrastructure.Data;
using Order.Infrastructure.Extensions;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.Services;
using Order.Infrastructure.Services.Refit;

namespace Order.Infrastructure;

public static class RentalInfastructureBootstrapper
{
    public static IServiceCollection AddRentalInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransitService<OrderDbContext>(configuration);
        services.AddSingleton<PublishDomainEventInterceptor>();
        services.AddDbContextPool<OrderDbContext>((sp, opt) =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefatultDatabase"));
            opt.AddInterceptors(sp.GetRequiredService<PublishDomainEventInterceptor>());
        });
        services.AddRefitClient<IInventoryApi>().ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://localhost:7010/api/Inventory");
        });
        services.AddScoped<IOrderRepository, RentalRepository>();
        services.AddScoped<IOrderService, RentalService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;

    }
}
