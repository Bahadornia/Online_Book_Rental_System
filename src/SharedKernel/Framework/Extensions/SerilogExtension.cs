using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace Framework.Extensions;

public static class SerilogExtension
{
    public static IHostBuilder UseSerilogBuilder(this IHostBuilder builder, string prefix)
    {
       return builder.UseSerilog((context, configuration) =>
        {
            configuration.Enrich.FromLogContext()
           .Enrich.WithMachineName()
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearchConfiguration:Uri"]!))
                {
                    IndexFormat = $"{prefix}-{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-logs{DateTime.UtcNow:yyyy-MM}",
                    NumberOfReplicas = 1,
                    NumberOfShards = 2,
                    AutoRegisterTemplate = true,
                });
        });
    }

    public static IApplicationBuilder UseSerilog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}
