namespace Website.Dtos
{
    public class BookFilterDto
    {
        public string Title { get; init; } = default!;
        public string Author { get; init; } = default!;
        public string Publisher { get; init; } = default!;
        public string Category { get; init; } = default!;
        public long ISBN { get; init; }
    }
}
