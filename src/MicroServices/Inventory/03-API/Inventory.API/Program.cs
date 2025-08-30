using Framework.Extensions;
using Inventory.API.Policies;
using Inventory.API.Policies.Requirements;
using Inventory.ApplicationServices;
using Inventory.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using SharedKernel.Messaging.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AtLeast18", opt =>
    {
        opt.Requirements.Add(new MinimumAgeRequirement(int.Parse(builder.Configuration.GetValue<string>("minimumAge")!)));
    });
});
builder.Services.AddSwaggerGen();
builder.Services.AddMessagingServices();
builder.Services.AddInventoryApplicationServices();
builder.Host.AddSerilogService("InventoryApp");
builder.Services.AddInventoryInfrastuctureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
