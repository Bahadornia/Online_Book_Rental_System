using ProtoBuf;

namespace Catalog.API.Grpc.Client.Responses;

[ProtoContract]
public class GetBookRs
{
    [ProtoMember(1)]
    public Guid Id { get; set; }

    [ProtoMember(2)]
    public string Title { get; init; } = default!;

    [ProtoMember(3)]
    public string Author { get; init; } = default!;

    [ProtoMember(4)]
    public int PublisherId { get; init; }

    [ProtoMember(5)]
    public int CategoryId { get; init; }

    [ProtoMember(6)]
    public long ISBN { get; init; }

    [ProtoMember(7)]
    public string Description { get; init; } = default!;

    [ProtoMember(8)]
    public string ImageUrl { get; set; } = default!;
}
