using Catalog.Infrastructure.Data.BookAggregate;
using MassTransit.MongoDbIntegration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogDbContext
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _db;
    private readonly MongoDbContext _context;
    public CatalogDbContext(IMongoClient client, MongoDbContext context)
    {
        _client = client;
        _db = _client.GetDatabase("CatalogDb");
        _context = context;
    }

    public IMongoClient Client => _client;
    public IMongoDatabase Database => _db;
    public IClientSessionHandle? Session => _context.Session;
    public IMongoCollection<BookData> Books => _db.GetCollection<BookData>("Book");
}

