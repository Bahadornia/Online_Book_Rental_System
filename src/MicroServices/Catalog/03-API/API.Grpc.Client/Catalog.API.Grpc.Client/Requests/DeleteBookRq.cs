using ProtoBuf;

namespace Catalog.API.Grpc.Client.Requests
{
    [ProtoContract]
    public class DeleteBookRq
    {
        [ProtoMember(1)]
        public long BookId { get; set; }
    }
}
