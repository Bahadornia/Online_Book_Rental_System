using Framework;
using Framework.CQRS;
using Microsoft.Extensions.DependencyInjection;
namespace Framework.Extensions;

public static class MediatRExtenstion
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        return services;
    }
}
