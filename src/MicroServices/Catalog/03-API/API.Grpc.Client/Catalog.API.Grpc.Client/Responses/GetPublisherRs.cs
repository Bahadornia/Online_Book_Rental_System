using ProtoBuf;

namespace Catalog.API.Grpc.Client.Responses;

[ProtoContract]
public sealed class GetPublisherRs
{
    [ProtoMember(1)]
    public string Name { get; set; } = default!;
}
