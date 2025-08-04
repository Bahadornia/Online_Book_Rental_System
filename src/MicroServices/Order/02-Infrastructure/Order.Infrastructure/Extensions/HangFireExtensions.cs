using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Services;

namespace Order.Infrastructure.Extensions;

public static class HangFireExtensions
{
    public static IServiceCollection AddHangFireServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<OrdersJob>();
        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                  .UseSimpleAssemblyNameTypeSerializer()
                  .UseRecommendedSerializerSettings()
                  .UseSqlServerStorage(configuration.GetConnectionString("DefatultDatabase"));
            RecurringJob.AddOrUpdate<OrdersJob>(OrdersJob.ORDERS_JOBID,
                x => x.Execute(default), OrdersJob.ORDERS_JOB_CRON);
        });
        services.AddHangfireServer();
        return services;
    }
}
