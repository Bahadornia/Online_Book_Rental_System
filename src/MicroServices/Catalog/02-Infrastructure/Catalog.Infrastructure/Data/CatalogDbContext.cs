using Catalog.Infrastructure.Data.BookAggregate;
using MassTransit.MongoDbIntegration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogDbContext
{
    private const string BOOK_COLLECTION_NAME = "Book";
    private const string PUBLISHER_COLLECTION_NAME = "Publisher";
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
    public IMongoCollection<BookData> Books => _db.GetCollection<BookData>(BOOK_COLLECTION_NAME);
    //public IMongoCollection<string> Publishers => _db.GetCollection<string>(PUBLISHER_COLLECTION_NAME);

    public async Task InitializeMongoDb()
    {
        var collectionName = BOOK_COLLECTION_NAME;
        var collectionList = await _db.ListCollectionNames().ToListAsync();
        if (!collectionList.Contains(collectionName))
        {
            var schema = new BsonDocument
            {
                { "bsonType", "object" },
                { "required", new BsonArray { "title", "author","isbn","publisher","category","availableCopies" } },
                { "properties", new BsonDocument
                    {
                        { "title", new BsonDocument { { "bsonType", "string" }, { "description", "must be a string and is required" } } },
                        { "author", new BsonDocument { { "bsonType", "string" }, { "description", "must be a string and is required" } } },
                        { "publisher", new BsonDocument {
                            { "bsonType", "string" },
                            { "required", new BsonArray { "name"} },
                            { "properties", new BsonDocument
                                {
                                    { "name", new BsonDocument { { "bsonType", "string" }, { "description", "must be a string and is required" } } },
                                }
                            },
                            { "description", "publisher is required and must be an object" }
                        } },
                        { "category", new BsonDocument {
                            { "bsonType", "string" },
                            { "required", new BsonArray { "name"} },
                            { "properties", new BsonDocument
                                {
                                    { "name", new BsonDocument { { "bsonType", "string" }, { "description", "category name is required" } } },
                                }
                            },
                            { "description", "category is required and must be an object" }
                        } },
                    { "isbn", new BsonDocument { { "bsonType", "string" }, { "description", "must be a long and is required" },{"minLength", 13 },{"maxLength",13 } } },
                         { "availableCopies", new BsonDocument { { "bsonType", "int" }, { "description", "must be a int and is required" } } }
                    }
                }
            };
            var options = new CreateCollectionOptions<BsonDocument>
            {

                Validator = new BsonDocumentFilterDefinition<BsonDocument>(
                    new BsonDocument
                    {
                        { "$jsonSchema", schema }
                    }
                )
            };

            await _db.CreateCollectionAsync(collectionName, options);
        }


        var indexes = Builders<BookData>.IndexKeys
            //.Ascending(item => item.Title)
            //.Ascending(item => item.Author)
            //.Ascending(item => item.Publisher)
            //.Ascending(item => item.Category)
            .Ascending(item => item.ISBN);
        
        var indexModel = new CreateIndexModel<BookData>(indexes);
        await _db.GetCollection<BookData>(collectionName).Indexes.CreateOneAsync(indexModel);
    }
}

