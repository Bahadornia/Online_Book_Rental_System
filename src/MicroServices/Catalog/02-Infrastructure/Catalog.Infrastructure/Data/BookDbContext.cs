using MongoDB.Driver;
using Catalog.Domain.Models.BookAggregate.Entities;

namespace Catalog.Infrastructure.Data;

public class BookDbContext
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _db;

    public BookDbContext(IMongoClient client)
    {
        _client = client;
        _db = _client.GetDatabase("CatalogDb");

    }

    public IMongoClient Client => _client;
    public IMongoDatabase Database => _db;
    public IMongoCollection<Book> Books => _db.GetCollection<Book>("Book");
}

