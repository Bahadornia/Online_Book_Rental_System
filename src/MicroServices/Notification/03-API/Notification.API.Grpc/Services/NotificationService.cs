using Notification.API.Grpc.Client.Logics;
using Notification.API.Grpc.Client.Requests;
using ProtoBuf.Grpc;

namespace Notification.API.Grpc.Services;

public class NotificationService : INotificationService
{
    public Task<IReadOnlyCollection<GetNotificationRs>> GetAll(CallContext context = default)
    {
        throw new NotImplementedException();
    }
}
