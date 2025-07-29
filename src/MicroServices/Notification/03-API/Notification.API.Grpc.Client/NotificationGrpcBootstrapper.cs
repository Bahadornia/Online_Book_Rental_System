using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.API.Grpc.Client.Logics;
using ProtoBuf.Grpc.Client;

namespace Notification.API.Grpc.Client;

public static class NotificationGrpcBootstrapper
{
    public static IServiceCollection AddNotificationGrcClient(this IServiceCollection services, string grpcAddress)
    {
        var defaultMethodConfig = new MethodConfig
        {
            Names = { MethodName.Default, },
            RetryPolicy = new RetryPolicy
            {
                MaxAttempts = 5,
                InitialBackoff = TimeSpan.FromSeconds(3),
                MaxBackoff = TimeSpan.FromSeconds(3),
                BackoffMultiplier = 1,
                RetryableStatusCodes =
                {
                    StatusCode.Internal, StatusCode.Unauthenticated, StatusCode.NotFound,
                    StatusCode.Unavailable
                },
            }
        };

        var channel = GrpcChannel.ForAddress(grpcAddress, new GrpcChannelOptions { ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig, } }, });
        services.AddSingleton(p =>
        {
            var client = channel.CreateGrpcService<INotificationService>();
            return client;
        });
        return services;
    }
}
