using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Framework.Extensions;

public static class RedisExtensions
{
    public static IServiceCollection AddRedisServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(p =>
        {
            var redisConnectionString = configuration.GetConnectionString("Redis");
            if (string.IsNullOrEmpty(redisConnectionString))
            {
                throw new InvalidOperationException("Redis connection string is not configured.");
            }
            return ConnectionMultiplexer.Connect(redisConnectionString);
        });
        return services;
    }
}
