using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.API.Grpc.Client.Requests
{
    [ProtoContract]
    public class GetBookImageRq
    {
        [ProtoMember(1)]
        public string FileName { get; set; } = default!;
    }
}
