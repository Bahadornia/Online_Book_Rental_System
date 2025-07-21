using Framework.Extensions;
using ProtoBuf.Grpc.Server;
using Order.API.Grpc.Services;
using Order.ApplicationServices;
using Order.Infrastructure;
using SharedKernel.Messaging.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Host.AddSerilogService("RentalApp");
builder.Services.AddMessagingServices();
builder.Services.AddRentalInfrastructureServices(builder.Configuration);
builder.Services.AddRentalApplicationServies();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<OrderGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
