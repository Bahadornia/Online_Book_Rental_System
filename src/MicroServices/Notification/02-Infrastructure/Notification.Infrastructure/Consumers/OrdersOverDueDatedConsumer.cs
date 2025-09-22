using MassTransit;
using Notification.Domain.Dtos;
using Notification.Domain.Enums;
using Notification.Domain.IRepositories;
using Notification.Domain.Models.Entities;
using Notification.Infrastructure.Data;
using SharedKernel.Messaging.Events;
using System.Text.Json;

namespace Notification.Infrastructure.Consumers;

public class OrdersOverDueDatedConsumer : IConsumer<OrdersOverDueDatedIntegrationEvent>
{
    private readonly IUnitofWork _unitOfWork;
    private readonly NotificationDbContext _dbContext;
    private readonly INotificationRepository _notificationRepositoy;
    private readonly IOutboxMessgeRepository _outboxMessgeRepository;

    public OrdersOverDueDatedConsumer(IUnitofWork unitOfWork, INotificationRepository repositoy, IOutboxMessgeRepository outboxMessgeRepository, NotificationDbContext dbContext)
    {
        _unitOfWork = unitOfWork;
        _notificationRepositoy = repositoy;
        _outboxMessgeRepository = outboxMessgeRepository;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<OrdersOverDueDatedIntegrationEvent> context)
    {
        var data = context.Message.Data;
        var ct = context.CancellationToken;
        if (!string.IsNullOrWhiteSpace(context.Message.Data))
        {
            var orders = JsonSerializer.Deserialize<IReadOnlyCollection<OverDueDatedOrdersDto>>(context.Message.Data);

            if (orders is not null && orders.Count > 0)
            {
                var outBoxMessage = new OutboxMessage
                {
                    Payload = context.Message.Data,
                    CreatedAt = DateTime.UtcNow,
                    Type = orders.GetType().ToString(),
                };
                var notification = new NotificationEntity
                {
                    CretedAt = DateTime.UtcNow,
                    Message = context.Message.Data,
                    MessageType = orders.GetType().ToString(),
                    Priority = Priority.HIGH,
                    Type = EventType.ORDER_OVER_DUEDATE,
                };
                try
                {
                    await _dbContext.Database.BeginTransactionAsync(ct);
                    await _notificationRepositoy.Add(notification, ct);
                    await _outboxMessgeRepository.Add(outBoxMessage, ct);
                    await _dbContext.Database.CommitTransactionAsync(ct);

                }
                catch (Exception)
                {

                    await _dbContext.Database.RollbackTransactionAsync(context.CancellationToken);
                }
                //await _hubContext.Clients.All.SendOverDuetedOrderNotification(orders);
            }
        }
    }
}
