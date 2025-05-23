using ProtoBuf.Grpc;
using Rental.API.Grpc.Client.Requests;
using System.ServiceModel;

namespace Rental.API.Grpc.Client.Logics;

[ServiceContract]
public interface IRentalGrpcService
{

    [OperationContract]
    Task BorrowBook(BorrowBookRq rq, CallContext context = default!);
}
