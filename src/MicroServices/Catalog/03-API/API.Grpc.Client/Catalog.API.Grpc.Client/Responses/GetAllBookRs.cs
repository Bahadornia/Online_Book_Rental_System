using ProtoBuf;

namespace Catalog.API.Grpc.Client.Responses;

[ProtoContract]
public class GetAllBookRs
{
    [ProtoMember(1)]
    public IReadOnlyCollection<GetBookRs> Books { get; set; } = default!;
    [ProtoMember(2)]
    public long TotalCount { get; set; }
}
