using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.API.Grpc.Client.Requests
{
    [ProtoContract]
    public class BookFilterRq
    {
        [ProtoMember(1)]
        public string Title { get; init; } = default!;
        [ProtoMember(2)]
        public string Author { get; init; } = default!;
        [ProtoMember(3)]
        public string Publisher { get; init; } = default!;
        [ProtoMember(4)]
        public string Category { get; init; } = default!;
        [ProtoMember(5)]
        public string ISBN { get; init; }
    }
}
