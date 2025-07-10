using Catalog.Domain.Models.BookAggregate.Events;
using Catalog.Domain.Models.BookAggregate.ValueObjects;
using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.Entities;

public class Book : AggregateRoot<BookId>
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Publisher { get; set; } = default!;
    public string Category { get; set; } = default!;
    public ISBN ISBN { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int AvailableCopies { get; set; }

    private Book() { }

    public static Book Create(BookId id, string title, string author, string publisherName, string categoryName, ISBN isbn, string desctiption, string image, int availableCopies)
    {

        
        var book = new Book
        {
            Id = id,
            Title = title,
            Author = author,
            Publisher = publisherName,
            Category = categoryName,
            ISBN = isbn,
            ImageUrl = image,
            Description = desctiption,
            AvailableCopies = availableCopies,
        };
        book.Emit(new BookAdded(book));
        return book;
    }

    public void Update(string title, string author, string publisherName, string categoryName, string isbn, string desctiption, string image, int availableCopies)
    {
        var publisher = publisherName;
        var category = categoryName;
        Title = title;
        Author = author;
        Publisher = publisher;
        Category = category;
        ISBN = isbn;
        Description = desctiption;
        ImageUrl = image;
        AvailableCopies = availableCopies;
        Emit(new BookUpdated(this));
    }

    protected override void ValidateInvariants()
    {
        return;
    }
}
