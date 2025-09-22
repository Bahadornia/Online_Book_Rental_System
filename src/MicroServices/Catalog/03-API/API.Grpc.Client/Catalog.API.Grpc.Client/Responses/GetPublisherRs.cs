using ProtoBuf;

namespace Catalog.API.Grpc.Client.Responses;

[ProtoContract]
public sealed class GetPublisherRs
{
    [ProtoMember(1)]
    public long Id { get; set; } = default!;

    [ProtoMember(2)]
    public string Name { get; set; } = default!;
}
