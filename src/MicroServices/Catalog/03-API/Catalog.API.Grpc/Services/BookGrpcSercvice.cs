using Grpc.Core;
using Catalog.API.Grpc;
using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using ProtoBuf.Grpc;

namespace Catalog.API.Grpc.Services;

public class BookGrpcSercvice : IBookGrpcService
{
    public Task AddBook(AddBookRq rq, CallContext callContext)
    {
        throw new NotImplementedException();
    }
}
