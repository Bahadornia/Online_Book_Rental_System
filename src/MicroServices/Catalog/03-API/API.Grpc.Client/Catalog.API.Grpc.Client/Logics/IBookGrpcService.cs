using Catalog.API.Grpc.Client.Requests;
using Catalog.API.Grpc.Client.Responses;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Catalog.API.Grpc.Client.Logics
{
    [ServiceContract]
    public interface IBookGrpcService
    {
        [OperationContract]
        Task AddBook(AddBookRq rq, CallContext callContext = default);
        
        [OperationContract]
        Task<GetBookImageRs> GetBookImage(GetBookImageRq rq, CallContext callContext = default);

        [OperationContract]
        Task<IReadOnlyCollection<GetBookRs>> GetAllBooks(CallContext callContext = default);
    }
}
