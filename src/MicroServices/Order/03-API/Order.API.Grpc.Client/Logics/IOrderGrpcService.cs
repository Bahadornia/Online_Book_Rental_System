using ProtoBuf.Grpc;
using Order.API.Grpc.Client.Requests;
using System.ServiceModel;

namespace Order.API.Grpc.Client.Logics;

[ServiceContract]
public interface IRentalGrpcService
{

    [OperationContract]
    Task RentBook(RentBookRq rq, CallContext context = default!);
}
