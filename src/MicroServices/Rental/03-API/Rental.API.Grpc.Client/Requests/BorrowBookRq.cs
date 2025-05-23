using ProtoBuf;

namespace Rental.API.Grpc.Client.Requests;

[ProtoContract]
public record BorrowBookRq
{
    [ProtoMember(1)]
    public long BookId { get; init; }

    [ProtoMember(2)]
    public long UserId { get; init; }

    [ProtoMember(3)]
    public DateTime BorrowDate { get; set; }
}
