using Framework;
using Framework.CQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Framework.Extensions;

public static class MediatRExtenstion
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        return services;
    }
}
