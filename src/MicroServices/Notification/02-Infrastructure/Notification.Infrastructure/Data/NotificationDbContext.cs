using MongoDB.Driver;
using Notification.Domain.Models.Entities;

namespace Notification.Infrastructure.Data;

public sealed class NotificationDbContext
{
    private const string DATABASE_NAME = "NotificatiosDb";
    private const string NOTIFICATION_COLLECTION_NAME = "Notificatios";
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _db;

    public NotificationDbContext(IMongoClient client, IMongoDatabase db)
    {
        _client = client;
        _db = _client.GetDatabase(DATABASE_NAME);
    }

    public IMongoClient Client => _client;
    public IMongoDatabase Database => _db;
    public IMongoCollection<NotificationEntity> Notifications => _db.GetCollection<NotificationEntity>(NOTIFICATION_COLLECTION_NAME); 
}
