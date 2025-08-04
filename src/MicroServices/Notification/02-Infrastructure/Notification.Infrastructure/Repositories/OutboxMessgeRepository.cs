using MongoDB.Driver;
using Notification.Domain.IRepositories;
using Notification.Domain.Models.Entities;
using Notification.Infrastructure.Data;

namespace Notification.Infrastructure.Repositories;

internal class OutboxMessgeRepository : IOutboxMessgeRepository
{
    private readonly NotificationDbContext _dbContext;

    public OutboxMessgeRepository(NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(OutboxMessage message, CancellationToken ct)
    {
        await _dbContext.OutboxMessages.InsertOneAsync(_dbContext.Session, message, null, ct);
    }

    public async Task<IReadOnlyCollection<OutboxMessage>> GetOnProcessedMessages(CancellationToken ct)
    {
        var builder = Builders<OutboxMessage>.Filter;
        var filter = builder.Eq(m => m.ProcessedAt, null);
        var messages =await  _dbContext.OutboxMessages.Find(filter).ToListAsync(ct);
        return messages;
    }

    async Task IOutboxMessgeRepository.UpdateProcessesOn(IReadOnlyCollection<OutboxMessage> messages, CancellationToken ct)
    {
        var bulkOpts = messages.Select(item => new UpdateOneModel<OutboxMessage>(
            Builders<OutboxMessage>.Filter.Eq(m => m.Id, item.Id),
            Builders<OutboxMessage>.Update.Set(m => m.ProcessedAt, DateTime.Now)));

        await _dbContext.OutboxMessages.BulkWriteAsync(bulkOpts, null, ct);
    }
}
