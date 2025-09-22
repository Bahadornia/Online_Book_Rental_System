using ProtoBuf;

namespace Catalog.API.Grpc.Client.Requests;

[ProtoContract]
public class AddBookRq
{
    [ProtoMember(1)]
    public string Title { get; set; } = default!;

    [ProtoMember(2)]
    public string Author { get; set; } = default!;

    [ProtoMember(3)]
    public int PublisherId { get; set; } = default!;

    [ProtoMember(4)]
    public int CategoryId { get; set; } = default!;

    [ProtoMember(5)]
    public string ISBN { get; set; } = default!;

    [ProtoMember(6)]
    public byte[] Image { get; set; } = default!;

    [ProtoMember(7)]
    public string? Description { get; set; }

    [ProtoMember(8)]
    public string ContentType { get; set; } = "image/jpeg";

    [ProtoMember(9)]
    public int AvailableCopies { get; set; }

    [ProtoMember(10)]
    public string PubliserName { get; set; } = default!;

    [ProtoMember(11)]
    public string CategoryName { get; set; } = default!;
}
