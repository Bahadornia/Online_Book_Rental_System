using Catalog.API.Grpc.Services;
using ProtoBuf.Grpc.Server;
using Catalog.Infrastructure;
using Framework.Extensions;
using Catalog.ApplicationServices;
using Catalog.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Services.AddCatalogApplicationServices();
builder.Services.AddCatalogInfrastructureServices(builder.Configuration);
builder.Host.AddSerilogService("CatalogApp");
builder.Services.AddRedisServices(builder.Configuration);


var app = builder.Build();
app.UseSerilog();
// Configure the HTTP request pipeline.
app.MapGrpcService<BookGrpcSercvice>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
Initialize(app);

void Initialize(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    dbContext.InitializeMongoDb().GetAwaiter().GetResult();
}

app.Run();
