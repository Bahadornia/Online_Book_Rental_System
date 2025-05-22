using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Extensions;

public static class MapsterExtension
{
    public static IServiceCollection AddMapsterService(this IServiceCollection services)
    {
        MapsterConfig.RegisterMapsterConfigurations();
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
