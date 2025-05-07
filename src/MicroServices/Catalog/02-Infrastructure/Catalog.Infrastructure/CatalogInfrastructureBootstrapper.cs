using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Catalog.Infrastructure;

public static class CatalogInfrastructureBootstrapper
{
    public static IServiceCollection AddCatalogInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("Database")));

        services.AddScoped<BookDbContext>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddMapsterService();

        return services;
    }

}
