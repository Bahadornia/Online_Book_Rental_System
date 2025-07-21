using Order.API.Grpc.Client.Enum;
using ProtoBuf;

namespace Order.API.Grpc.Client.Responses;

[ProtoContract]
public class GetOrderRs
{
    [ProtoMember(1)]
    public long OrderId { get; set; } = default!;
    [ProtoMember(2)]
    public long BookId { get; set; } = default!;
    [ProtoMember(3)]
    public string BookTitle { get; set; } = default!;
    [ProtoMember(4)]
    public string ISBN { get; set; } = default!;
    [ProtoMember(5)]
    public DateTime RentDate { get; set; }
    [ProtoMember(6)]
    public DateTime ReturnDate { get; set; }
    [ProtoMember(7)]
    public int NumberOfExtending { get; set; }
    [ProtoMember(8)]
    public OrderStatus Status { get; set; }
    [ProtoMember(9)]
    public string FullName { get; set; } = default!;
}
