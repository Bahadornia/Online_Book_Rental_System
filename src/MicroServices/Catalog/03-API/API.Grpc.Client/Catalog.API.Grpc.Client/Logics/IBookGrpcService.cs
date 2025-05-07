using Catalog.API.Grpc.Client.Requests;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using System.ServiceModel;

namespace Catalog.API.Grpc.Client.Logics
{
    [ServiceContract]
    public interface IBookGrpcService
    {
        [OperationContract]
        Task AddBook(AddBookRq rq, CallContext callContext);
    }
}
