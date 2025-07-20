using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Order.API.Grpc.Client.Logics;

namespace Order.API.Grpc.Client;

public static class RentalGrpcBootstrapper
{
    public static IServiceCollection AddRentalGrpcClient(this IServiceCollection services, string grpcAddress) 
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
                },
            }
        };

        var channel = GrpcChannel.ForAddress(grpcAddress, new GrpcChannelOptions { ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig, } }, });
        services.AddSingleton(p =>
        {
            var client = channel.CreateGrpcService<IRentalGrpcService>();
            return client;
        });
        return services;

    }
}
