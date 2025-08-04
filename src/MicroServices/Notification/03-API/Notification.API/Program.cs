using Notification.Infrastructure;
using Notification.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddNotificationInfratructureServices(builder.Configuration);
builder.Services.AddMassTransitService(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseNotificationServices();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHubRoutes("/OrderNotification");

app.Run();
