using System.ComponentModel;

namespace Website.Dtos
{
    public sealed class GridResponseDto<T>
    {
        [DisplayName("rows")]
        public object Rows { get; set; }
        [DisplayName("totalCount")]
        public int TotalCount { get; set; }
    }
}
