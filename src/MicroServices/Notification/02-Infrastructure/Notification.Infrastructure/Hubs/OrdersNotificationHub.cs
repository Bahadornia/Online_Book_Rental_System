using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Notification.Domain.Models.Entities;
using System.Security.Claims;

namespace Notification.Infrastructure.Hubs;

[Authorize(Policy = "notifications")]
internal sealed class OrdersNotificationHub : Hub<INotificatinClient>
{

    public string UserId => Context.UserIdentifier;

    public override async Task OnConnectedAsync()
    {
        if (Context.User.IsInRole("admin"))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "admins");
        }
        await base.OnConnectedAsync();


    }

}
public interface INotificatinClient
{
    Task SendOrderCreatedNotification(object obj);
    Task SendOverDuetedOrderNotification(IReadOnlyCollection<OutboxMessage> dto);

}
