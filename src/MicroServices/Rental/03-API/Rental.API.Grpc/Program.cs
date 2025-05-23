using Framework.Extensions;
using ProtoBuf.Grpc.Server;
using Rental.API.Grpc.Services;
using Rental.ApplicationServices;
using Rental.Infastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Services.AddMediatRServices();
builder.Services.AddRentalInfrastructureServices(builder.Configuration);
builder.Services.AddRentalApplicationServies();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<RentalGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
