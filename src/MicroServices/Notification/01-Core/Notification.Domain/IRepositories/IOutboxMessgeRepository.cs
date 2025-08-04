using Notification.Domain.Models.Entities;

namespace Notification.Domain.IRepositories;

public interface IOutboxMessgeRepository
{
    Task Add(OutboxMessage message, CancellationToken ct = default);
    Task UpdateProcessesOn(IReadOnlyCollection<OutboxMessage> messages, CancellationToken ct = default);
    Task<IReadOnlyCollection<OutboxMessage>> GetOnProcessedMessages(CancellationToken ct = default);
}
