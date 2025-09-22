using Microsoft.EntityFrameworkCore;
using Notification.Domain.IRepositories;
using Notification.Domain.Models.Entities;
using Notification.Infrastructure.Data;

namespace Notification.Infrastructure.Repositories;

internal class OutboxMessgeRepository : IOutboxMessgeRepository
{
    private readonly NotificationDbContext _dbContext;
    private readonly IUnitofWork _unitofWork;

    public OutboxMessgeRepository(NotificationDbContext dbContext, IUnitofWork unitofWork)
    {
        _dbContext = dbContext;
        _unitofWork = unitofWork;
    }

    public async Task Add(OutboxMessage message, CancellationToken ct)
    {
        _dbContext.OutboxMessages.Add(message);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyCollection<OutboxMessage>> GetOnProcessedMessages(CancellationToken ct = default)
    {
        return await _dbContext.OutboxMessages.Where(m=> m.ProcessedAt == null).ToListAsync(ct);
    }

    public async Task UpdateProcessesOn(IReadOnlyCollection<OutboxMessage> messages, CancellationToken ct = default)
    {
        var dict = messages.ToDictionary(m => m.Id);
        var ids= dict.Keys;
        await _dbContext.OutboxMessages.Where(m => ids.Contains(m.Id)).ExecuteUpdateAsync(s => s.SetProperty(m => m.ProcessedAt , DateTime.UtcNow), ct);
    }

    async Task IOutboxMessgeRepository.UpdateProcessesOn(CancellationToken ct)
    {

        await _dbContext.OutboxMessages.Where(n => n.ProcessedAt == null).ExecuteUpdateAsync(s => s.SetProperty(n => n.ProcessedAt, DateTime.UtcNow), ct);
    }
}
