using ProtoBuf;

namespace Order.API.Grpc.Client.Requests;

[ProtoContract]
public record OrderBookRq
{
    [ProtoMember(1)]
    public long BookId { get; init; }

    [ProtoMember(2)]
    public string UserId { get; init; }

    [ProtoMember(3)]
    public DateTime BorrowDate { get; set; }
}
