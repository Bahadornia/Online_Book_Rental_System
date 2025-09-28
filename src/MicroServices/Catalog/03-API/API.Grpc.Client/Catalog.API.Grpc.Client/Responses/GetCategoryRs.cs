using ProtoBuf;

namespace Catalog.API.Grpc.Client.Responses;

[ProtoContract]
public sealed class GetCategoryRs
{
    [ProtoMember(1)]
    public int Id { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; } = default!;
}
