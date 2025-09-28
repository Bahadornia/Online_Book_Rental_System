using Catalog.API.Grpc.Client.Responses;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Catalog.API.Grpc.Client.Logics
{
    [ServiceContract]
    public interface ICategoryService
    {
        [OperationContract]
        Task<IReadOnlyCollection<GetCategoryRs>> GetAll(string term, CallContext ct = default);
    }
}
