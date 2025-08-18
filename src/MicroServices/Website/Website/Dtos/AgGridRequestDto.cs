using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Website.Dtos
{
    public class AgGridRequestDto
    {
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public List<AgSortModelDto>? SortModel { get; set; }
        public Dictionary<string, AgFilterModelDto>? FilterModel { get; set; }
    }
    public class AgSortModelDto
    {
        public string ColId { get; set; } = default!;
        public string Sort { get; set; } = "asc"; // "asc" | "desc"
    }

    public class AgFilterModelDto
    {
        public string? Type { get; set; } // equals, notEqual, contains, startsWith, greaterThan, etc.
        public string? Filter { get; set; }
        public string? FilterType { get; set; } // "text" | "number" | "date"
    }
}
