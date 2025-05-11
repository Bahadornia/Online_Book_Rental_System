namespace Website.Models
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; init; } = default!;
        public string Author { get; init; } = default!;
        public string PublisherName { get; init; } = default!;
        public string CategoryName { get; init; } = default!;
        public long ISBN { get; init; }
        public string Description { get; init; } = default!;
        public string Image { get; set; } = default!;
    }
}
