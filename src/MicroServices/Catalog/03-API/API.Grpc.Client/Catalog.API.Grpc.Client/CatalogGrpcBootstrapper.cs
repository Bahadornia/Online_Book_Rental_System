using Catalog.API.Grpc.Client.Logics;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;

namespace Catalog.API.Grpc.Client;

public static class CatalogGrpcBootstrapper
{

    public static IServiceCollection AddCatalogGrpcClient(this IServiceCollection services, string grpcAddress)
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
            var client = channel.CreateGrpcService<IBookGrpcService>();
            return client;
        });
        return services;
    }
}
