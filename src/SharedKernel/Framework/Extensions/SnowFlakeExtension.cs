using Framework.SnowFlake;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Extensions
{
    public static class SnowflakeExtension
    {
        public static IServiceCollection AddSnowflakeService(this IServiceCollection services, int generatorId = 1234654897)
        {
            services.AddSingleton<ISnowFlakeService, SnowFlakeService>();
            return services;
        }
    }
}
