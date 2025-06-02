using Inventory.Domain.IRepositories;
using Inventory.Domain.IServices;
using Inventory.Infrastructure.Consumers;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Extensions;
using Inventory.Infrastructure.Repositories;
using Inventory.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Messaging;
using System.Runtime.Intrinsics.Arm;

namespace Inventory.Infrastructure;

public static class InventoryInfrastructureBootstrapper
{
    public static IServiceCollection AddInventoryInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IUnitofWork, UnitofWork>();
        services.AddMassTransitServices<InventoryDbContext>(configuration);
        services.AddDbContextPool<InventoryDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefatultDatabase"));
        });
        return services;
    }
}
