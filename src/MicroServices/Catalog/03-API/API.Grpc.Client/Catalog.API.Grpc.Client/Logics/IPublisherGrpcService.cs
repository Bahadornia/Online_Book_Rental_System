using Catalog.API.Grpc.Client.Responses;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Catalog.API.Grpc.Client.Logics;

[ServiceContract]
public interface IPublisherGrpcService
{
    [OperationContract]
    Task<IReadOnlyCollection<GetPublisherRs>> GetPublishers(string term, CallContext context = default);
}


