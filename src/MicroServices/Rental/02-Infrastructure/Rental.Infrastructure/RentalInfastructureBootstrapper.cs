using Framework.Interceptors;
using MassTransit;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rental.Domain.IRepositories;
using Rental.Domain.IServices;
using Rental.Infrastructure.Data;
using Rental.Infrastructure.Repositories;
using Rental.Infrastructure.Services;
using SharedKernel.Messaging.Extensions;

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
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IRentalService, RentalService>();
        return services;

    }
}
