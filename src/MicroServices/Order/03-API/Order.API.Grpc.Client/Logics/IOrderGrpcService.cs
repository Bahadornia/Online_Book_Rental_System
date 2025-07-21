using ProtoBuf.Grpc;
using Order.API.Grpc.Client.Requests;
using System.ServiceModel;
using Order.API.Grpc.Client.Responses;

namespace Order.API.Grpc.Client.Logics;

[ServiceContract]
public interface IOrderGrpcService
{

    [OperationContract]
    Task RentBook(OrderBookRq rq, CallContext context = default!);

    [OperationContract]
    Task<IReadOnlyCollection<GetOrderRs>> GetAll(CallContext context = default!);
}
