using ProtoBuf;

namespace Catalog.API.Grpc.Client.Requests
{
    [ProtoContract]
    public class AgGridRequestRq
    {
        [ProtoMember(1)]
        public int StartRow { get; set; }
        [ProtoMember(2)]
        public int EndRow { get; set; }
        [ProtoMember(3)]
        public List<AgSortModelRq>? SortModel { get; set; }
        [ProtoMember(4)]
        public Dictionary<string, AgFilterModelRq>? FilterModel { get; set; }
    }
    [ProtoContract]
    public class AgSortModelRq
    {
        [ProtoMember(1)]
        public string ColId { get; set; } = default!;
        [ProtoMember(2)]
        public string Sort { get; set; } = "asc"; // "asc" | "desc"
    }

    [ProtoContract]
    public class AgFilterModelRq
    {
        [ProtoMember(1)]
        public string? Type { get; set; } // equals, notEqual, contains, startsWith, greaterThan, etc.
        [ProtoMember(2)]
        public string? Filter { get; set; }
        [ProtoMember(3)]
        public string? FilterType { get; set; } // "text" | "number" | "date"
    }
}
