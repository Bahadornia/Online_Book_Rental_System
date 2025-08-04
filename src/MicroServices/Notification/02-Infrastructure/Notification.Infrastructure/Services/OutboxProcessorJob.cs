using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.Domain.IRepositories;
using Notification.Domain.Models.Entities;
using Notification.Infrastructure.Hubs;

namespace Notification.Infrastructure.Services;

internal sealed class OutboxProcessorJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<OrdersNotificationHub, INotificatinClient> _hubContext;

    public OutboxProcessorJob(IHubContext<OrdersNotificationHub, INotificatinClient> hubContext, IServiceScopeFactory scopeFactory)
    {
        _hubContext = hubContext;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IOutboxMessgeRepository>();
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            var messages = await repository.GetOnProcessedMessages(stoppingToken);
            if (messages.Count > 0)
            {
                try
                {
                    await _hubContext.Clients.All.SendOverDuetedOrderNotification(messages);
                    await UpdateMessages(repository, messages, stoppingToken);
                }
                catch (Exception)
                {

                    throw;
                }

            }

            //update
        }
    }

    private async Task UpdateMessages(IOutboxMessgeRepository repository, IReadOnlyCollection<OutboxMessage> messages, CancellationToken ct)
    {
        await repository.UpdateProcessesOn(messages, ct);
    }
}
