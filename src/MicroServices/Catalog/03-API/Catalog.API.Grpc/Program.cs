using Catalog.ApplicationServices;
using Catalog.API.Grpc.Services;
using ProtoBuf.Grpc.Server;
using Catalog.Infrastructure;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Services.AddApplicationServices();
builder.Services.AddCatalogInfrastructureServices(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.Enrich.FromLogContext()
   .Enrich.WithMachineName()
   .Enrich.FromLogContext()
   .WriteTo.Console()
   .WriteTo.Elasticsearch(
        new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearchConfiguration:Uri"]!))
        {
            IndexFormat = $"CatalogApp-{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-logs{DateTime.UtcNow:yyyy-MM}",
            NumberOfReplicas = 1,
            NumberOfShards = 2,
            AutoRegisterTemplate = true,
        });
});



var app = builder.Build();
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
app.MapGrpcService<BookGrpcSercvice>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
