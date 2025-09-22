using Catalog.Domain.Models.BookAggregate.Events;
using Catalog.Domain.Models.BookAggregate.ValueObjects;
using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.Entities;

public class Book : AggregateRoot<BookId>
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public PublisherId PublisherId { get; set; } = default!;
    public CategoryId CategoryId { get; set; } = default!;
    public ISBN ISBN { get; set; } = default!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int AvailableCopies { get; set; }

    public Category Category { get; set; }
    public Publisher Publiser { get; set; }

    private Book() { }

    public static Book Create(BookId id, string title, string author, int publisherId, int categoryId, ISBN isbn, string desctiption, string image, int availableCopies)
    {

        
        var book = new Book
        {
            Id = id,
            Title = title,
            Author = author,
            PublisherId = publisherId,
            CategoryId = categoryId,
            ISBN = isbn,
            ImageUrl = image,
            Description = desctiption,
            AvailableCopies = availableCopies,
        };
        book.Emit(new BookAdded(book));
        return book;
    }

    public void Update(string title, string author, int publisherId, int categoryId, string isbn, string desctiption, string image, int availableCopies)
    {
        Title = title;
        Author = author;
        PublisherId = publisherId;
        CategoryId = categoryId;
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
