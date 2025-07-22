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

        [OperationContract]
        Task DeleteBook(DeleteBookRq rq, CallContext callContext = default);
        
        [OperationContract]
        Task<IReadOnlyCollection<GetBookRs>> SearchBook(BookFilterRq rq, CallContext callContext = default);

        [OperationContract]
        Task<GetBookRs> GetById(GetBookRq rq, CallContext context = default);

        [OperationContract]
        Task<IReadOnlyCollection<GetBookRs>> GetBooksByIds(IEnumerable<long> ids, CallContext context = default);
    }
}
