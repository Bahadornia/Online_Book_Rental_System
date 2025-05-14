using ProtoBuf;

namespace Catalog.API.Grpc.Client.Responses;

[ProtoContract]
public class GetBookImageRs
{
    [ProtoMember(1)]
    public string Url { get; set; } = default!;
}
