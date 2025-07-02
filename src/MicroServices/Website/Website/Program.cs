using Catalog.API.Grpc.Client;
using Framework.Extensions;
using Rental.API.Grpc.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCatalogGrpcClient(builder.Configuration.GetValue<string>("CatalogGrpcService")!);
builder.Services.AddRentalGrpcClient(builder.Configuration.GetValue<string>("RentalGrpcService")!);
builder.Services.AddMapsterService(Assembly.GetExecutingAssembly());
builder.Services.AddHashids(opt => opt.Salt = "/-_#*+!?()=:.@");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
