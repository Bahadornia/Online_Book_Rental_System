using Library.Repository.Infrastructure.Data;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Library.Repository.Infrastructure;

public static class CatalogBootstrapper
{
    public static IServiceCollection AddMongoServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("Database")));

        services.AddScoped<BookDbContext>();

        return services;
    }
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        MapsterConfig.RegisterMapsterConfigurations();
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
