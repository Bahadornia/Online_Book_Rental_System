using ProtoBuf;

namespace Catalog.API.Grpc.Client.Responses;

[ProtoContract]
public class GetBookRs
{
    [ProtoMember(1)]
    public long Id { get; set; }

    [ProtoMember(2)]
    public string Title { get; init; } = default!;

    [ProtoMember(3)]
    public string Author { get; init; } = default!;

    [ProtoMember(4)]
    public string Publisher { get; init; } = default!;

    [ProtoMember(5)]
    public string Category { get; init; } = default!;

    [ProtoMember(6)]
    public long ISBN { get; init; }

    [ProtoMember(7)]
    public string Description { get; init; } = default!;

    [ProtoMember(8)]
    public string ImageUrl { get; set; } = default!;
}
