using Framework;
using Library.Repository.Domain.Models.BookAggregate.Events;
using Library.Repository.Domain.Models.BookAggregate.ValueObjects;

namespace Library.Repository.Domain.Models.BookAggregate.Entities;

public class Book : AggregateRoot<BookId>
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public int PublisherId { get; set; }
    public int CategoryId { get; set; }
    public ISBN ISBN { get; set; } = default!;
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
            PublisherId = publisherId,
            CategoryId = categoryId,
            ISBN = isbn,
            Image = image,
            Description = desctiption,
        };
        var bookAddedEvent = new BookAddedEvent(book);
        book.AddDomainEvents(bookAddedEvent);
        return book;
    }

    public void Update(string title, string author, int publisherId, int categoryId, long isbn, string desctiption, string image)
    {
        Title = title;
        Author = author;
        PublisherId = publisherId;
        CategoryId = categoryId;
        ISBN = isbn;
        Description = desctiption;
        Image = image;
    }

}
