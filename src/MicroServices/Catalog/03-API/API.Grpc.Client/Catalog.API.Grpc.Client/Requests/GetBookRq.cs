using ProtoBuf;

namespace Catalog.API.Grpc.Client.Requests;

[ProtoContract]
public class GetBookRq
{
    [ProtoMember(1)]
    public long Id { get; set; }
}
