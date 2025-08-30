using MassTransit.MongoDbIntegration;
using MongoDB.Driver;
using Notification.Domain.Models.Entities;
using Notification.Infrastructure.Models;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Data;

public sealed class NotificationDbContext
{
    private const string DATABASE_NAME = "NotificationDb";
    private const string NOTIFICATION_COLLECTION_NAME = "Notificatios";
    private const string OUT_BOX_COLLECTION_NAME = "OutboxMessages";
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _db;
    private readonly MongoDbContext _context;

    public NotificationDbContext(IMongoClient client, MongoDbContext context)
    {
        _client = client;
        _db = _client.GetDatabase(DATABASE_NAME);
        _context = context;
    }

    public IMongoClient Client => _client;
    public IMongoDatabase Database => _db;
    public IClientSessionHandle? Session => _context.Session;
    public IMongoCollection<NotificationEntity> Notifications => _db.GetCollection<NotificationEntity>(NOTIFICATION_COLLECTION_NAME);
    public IMongoCollection<OutboxMessage> OutboxMessages => _db.GetCollection<OutboxMessage>(OUT_BOX_COLLECTION_NAME); 

    public async Task InitializeMongoDb()
    {
        var proccessedAtIndex = Builders<OutboxMessage>.IndexKeys.Descending(n => n.ProcessedAt);
        var proccessedAtIndexModel = new CreateIndexModel<OutboxMessage>(proccessedAtIndex);
        await _db.GetCollection<OutboxMessage>(OUT_BOX_COLLECTION_NAME).Indexes.CreateOneAsync(proccessedAtIndexModel);
    }
}
