using Catalog.API.Grpc.Client;
using Duende.Bff.Yarp;
using Framework.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.IdentityModel.Tokens;
using Order.API.Grpc.Client;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using Yarp.ReverseProxy.Transforms;


var culutureInfo = new CultureInfo("fa-IR");
CultureInfo.CurrentCulture = culutureInfo;
var cult = CultureInfo.CurrentCulture.Name;
var builder = WebApplication.CreateBuilder(args);
var yarpBuilder = builder.Services.AddReverseProxy()
    .AddTransforms(context =>
    {
        context.AddRequestTransform(async transformContext =>
        {
            var accessToken = await transformContext.HttpContext.GetTokenAsync("access_token");
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                transformContext.ProxyRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
        });
    });
//Configure from included extension method
yarpBuilder.Configure();
builder.Services.AddUserAccessTokenHttpClient("hub_client", configureClient: client =>
{
    client.BaseAddress = new Uri("https://localhost:7101");
});
builder.Services.AddBff()
    .AddServerSideSessions()
    .AddRemoteApis();

// Add services to the container.
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
      .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
    .AddDataAnnotationsLocalization();
builder.Services.AddCatalogGrpcClient(builder.Configuration.GetValue<string>("CatalogGrpcService")!);
builder.Services.AddRentalGrpcClient(builder.Configuration.GetValue<string>("RentalGrpcService")!);
builder.Services.AddMapsterService(Assembly.GetExecutingAssembly());
builder.Services.AddHashids(opt => opt.Salt = "/-_#*+!?()=:.@");
var authConfigs = builder.Configuration.GetSection("Authentication");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(opt =>
    {
        opt.Cookie.SameSite = SameSiteMode.Lax;          // allow cross-site auth redirect
        opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        
    })
    .AddOpenIdConnect(opt =>
    {

        opt.Authority = authConfigs["Authority"];
        opt.ClientId = authConfigs["ClientId"];
        opt.ClientSecret = authConfigs["ClientSecret"];
        opt.SaveTokens = true; // Store access token in cookie
        opt.GetClaimsFromUserInfoEndpoint = true;
        opt.ResponseType = "code";
        opt.Scope.Add("openid");
        opt.Scope.Add("profile");
        opt.Scope.Add("roles");
        opt.Scope.Add("notifications.read");
        opt.ClaimActions.MapUniqueJsonKey("name", "name");
        opt.ClaimActions.MapUniqueJsonKey("preferred_username", "preferred_username");
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "preferred_username"
        };

    });
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var supportedCultures = new[]
{
    new CultureInfo("en"), // English (United States)
    new CultureInfo("fa")  // Farsi (Iran)
};

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fa"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();
app.MapBffManagementEndpoints();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapBffReverseProxy().RequireAuthorization();
app.Run();

public class CusotomMiddlewar : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}
