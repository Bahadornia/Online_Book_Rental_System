using ProtoBuf.Grpc;
using Rental.API.Grpc.Client.Requests;
using System.ServiceModel;

namespace Rental.API.Grpc.Client.Logics;

[ServiceContract]
public interface IRentalGrpcService
{

    [OperationContract]
    Task RentBook(RentBookRq rq, CallContext context = default!);
}
