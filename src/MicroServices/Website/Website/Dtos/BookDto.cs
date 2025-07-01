using ProtoBuf;

namespace Website.Dtos
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Title { get; init; } = default!;
        public string Author { get; init; } = default!;
        public string Publisher { get; init; } = default!;
        public string Category { get; init; } = default!;
        public long ISBN { get; init; }
        public string Description { get; init; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}
