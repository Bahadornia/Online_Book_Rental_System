using Microsoft.AspNetCore.SignalR;
using Notification.Domain.Dtos;
using Notification.Domain.Models.Entities;

namespace Notification.Infrastructure.Hubs;

internal sealed class OrdersNotificationHub: Hub<INotificatinClient>
{
    
}
public interface INotificatinClient
{
    Task SendOrderCreatedNotification(object obj);
    Task SendOverDuetedOrderNotification(IReadOnlyCollection<OutboxMessage> dto);
   
}
