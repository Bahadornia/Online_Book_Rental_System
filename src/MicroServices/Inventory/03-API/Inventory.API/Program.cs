using Inventory.Infrastructure;
using SharedKernel.Messaging.Extensions;
using Framework.Extensions;
using Inventory.ApplicationServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMessagingServices();
builder.Services.AddInventoryApplicationServices();
builder.Host.AddSerilogService("InventoryApp");
builder.Services.AddInventoryInfrastuctureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
