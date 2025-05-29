using MongoDB.Driver;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data.BookAggregate;

namespace Catalog.Infrastructure.Data;

public class CatalogDbContext
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _db;

    public CatalogDbContext(IMongoClient client)
    {
        _client = client;
        _db = _client.GetDatabase("CatalogDb");

    }

    public IMongoClient Client => _client;
    public IMongoDatabase Database => _db;
    public IMongoCollection<BookData> Books => _db.GetCollection<BookData>("Book");
}

