using Catalog.Domain.Model.Shared.Enums;
using Core;

namespace Catalog.Domain.Model.BookAggregate.Entities
{
    public class Book : AggregateRoot<BookId>
    {
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public int PulbisherId { get; set; }
        public int CategoryId { get; set; }
        public long ISBN { get; set; } = default!;
        public string? Description { get; set; }
        public string? Image { get; set; }

        private Book() { }

        public static Book Create(Guid id, string title, string author, int publisherId, int categoryId, long isbn, string desctiption, string image)
        {
            var book = new Book
            {
                Id = id,
                Title = title,
                Author = author,
                PulbisherId = publisherId,
                CategoryId = categoryId,
                ISBN = isbn,
                Image = image,
                Description = desctiption,
            };
            return book;
        }
    }
}
