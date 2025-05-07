using MongoDB.Driver;
using Library.Repository.Domain.Models.BookAggregate.Entities;

namespace Library.Repository.Infrastructure.Data;

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

