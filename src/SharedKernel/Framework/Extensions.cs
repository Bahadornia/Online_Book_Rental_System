using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework;

public static class Extensions
{
    public static IServiceCollection AddMapsterService(this IServiceCollection services)
    {
        MapsterConfig.RegisterMapsterConfigurations();
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
