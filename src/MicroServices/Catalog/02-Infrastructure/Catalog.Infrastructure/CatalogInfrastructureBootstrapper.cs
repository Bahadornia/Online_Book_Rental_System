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
using SharedKernel.Messaging.Extensions;
using Catalog.Infrastructure.Extensions;
using MongoDB.Driver.Core.Configuration;
using System.Reflection;

namespace Catalog.Infrastructure;

public static class CatalogInfrastructureBootstrapper
{
    public static IServiceCollection AddCatalogInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("Database")));

        services.AddScoped<CatalogDbContext>();
        services.AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetConnectionString("Database")));
        services.AddSingleton(provider => provider.GetRequiredService<IMongoClient>().GetDatabase("CatalogDb"));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.Decorate<IBookRepository, CachedBookRepository>();
        services.AddMapsterService(Assembly.GetExecutingAssembly());
        services.AddDomainServices();
        services.AddMassTransitService(configuration);
        services.AddMessagingServices();

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
