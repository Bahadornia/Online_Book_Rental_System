using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Services;
using Framework.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Catalog.Infrastructure.Extensions;
using System.Reflection;
using SharedKernel.Messaging.Extensions;

namespace Catalog.Infrastructure;

public static class CatalogInfrastructureBootstrapper
{
    public static IServiceCollection AddCatalogInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CatalogDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPubliserRepository, PublisherRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        services.Decorate<IBookRepository, CachedBookRepository>();
        services.Decorate<IPubliserRepository, CachedPublisherRepositroy>();
        services.AddMapsterService(Assembly.GetExecutingAssembly());
        services.AddDomainServices();
        services.AddMassTransitService<CatalogDbContext>(configuration);
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
