using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.Domain.IRepositories;
using Notification.Domain.Models.Entities;
using Notification.Infrastructure.Hubs;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace Notification.Infrastructure.Services;

internal sealed class OutboxProcessorJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<OrdersNotificationHub, INotificatinClient> _hubContext;

    public OutboxProcessorJob(IHubContext<OrdersNotificationHub, INotificatinClient> hubContext, IServiceScopeFactory scopeFactory, IUserIdProvider userProvider)
    {
        _hubContext = hubContext;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromDays(1));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IOutboxMessgeRepository>();
            var messages = await repository.GetOnProcessedMessages(stoppingToken);
            if(messages is not null && messages.Count > 0)
            {

            var groupedMessages = messages.GroupBy(m => m.UserId);
                foreach (var group in groupedMessages) { 
                try
                {
                    await _hubContext.Clients.User(group.Key).SendOverDuetedOrderNotification(group.ToArray());
                    await UpdateMessages(repository, messages, stoppingToken);
                }
                catch (Exception)
                {

                    throw;
                }
                }

           
            }
        }
    }

    private async Task UpdateMessages(IOutboxMessgeRepository repository, IReadOnlyCollection<OutboxMessage> messages, CancellationToken ct)
    {
        await repository.UpdateProcessesOn(messages, ct);
    }
}
