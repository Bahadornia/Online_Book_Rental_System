using ProtoBuf.Grpc;
using Rental.API.Grpc.Client.Logics;
using Rental.API.Grpc.Client.Requests;

namespace Rental.API.Grpc.Services;

public class RentalGrpcService : IRentalGrpcService
{
    public Task BorrowBook(BorrowBookRq rq, CallContext context = default)
    {
        throw new NotImplementedException();
    }
}
