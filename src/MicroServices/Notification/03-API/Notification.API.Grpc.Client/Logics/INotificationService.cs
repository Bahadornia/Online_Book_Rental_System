using Notification.API.Grpc.Client.Requests;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using System.ServiceModel;

namespace Notification.API.Grpc.Client.Logics;

[Service("NotificationService")]
public interface INotificationService
{
    [OperationContract]
    Task<IReadOnlyCollection<GetNotificationRs>> GetAll(CallContext context = default);
}
