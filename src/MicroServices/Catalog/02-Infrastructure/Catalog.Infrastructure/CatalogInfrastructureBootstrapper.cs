using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Services;
using Framework.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
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

        services.AddMinio(cfg =>
        {
            var config = configuration.GetSection("Minio");

            cfg.WithEndpoint(config["Endpoint"])
                .WithCredentials(config["AccessKey"], config["SecretKey"])
                .WithSSL(bool.Parse(config["WithSSL"]!))
                .Build();
        });
        services.AddSingleton<IFileService, FileService>();

        return services;
    }

}
