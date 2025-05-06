using Catalog.Domain.Model.BookAggregate.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class BookDbContext
{
    private readonly IMongoClient _client;
    public readonly IMongoDatabase _db;

    public BookDbContext(IMongoClient client)
    {
        _client = client;
        _db = _client.GetDatabase("CatalogDb");

    }
    public IMongoCollection<Book> Books => _db.GetCollection<Book>("Book");
}
