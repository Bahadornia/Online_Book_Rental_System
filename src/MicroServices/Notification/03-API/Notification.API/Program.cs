using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Notification.Infrastructure;
using Notification.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddNotificationInfratructureServices(builder.Configuration);
builder.Services.AddMassTransitService(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.Authority = builder.Configuration.GetValue<string>("JWT:Authority");
        opt.Audience = "notifications";
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
        };
        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["access_token"];
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("notifications", p => p.RequireClaim("scope", "notifications.read"));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseNotificationServices();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHubRoutes("/hub/OrderNotification");

app.InitializeNotifcationDb();
app.Run();
