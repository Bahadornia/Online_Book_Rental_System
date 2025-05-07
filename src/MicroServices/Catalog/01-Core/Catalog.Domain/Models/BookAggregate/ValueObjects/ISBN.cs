namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public record ISBN
{
    public long Value { get; set; }
    private ISBN(long isbn)
    {
        Value = isbn;
    }

    public static ISBN Create(long isbn)
    {

        ValidateIsbn(isbn);
        return new ISBN(isbn);
    }
    private static void ValidateIsbn(long isbn)
    {
        if (isbn < 0 && isbn.ToString().Length < 13) throw new Exception("Invalid ISBN");
    }

    public static implicit operator ISBN(long isbn)
    {
        return Create(isbn);
    }

    public static implicit operator long(ISBN isbn)
    {
        return isbn.Value;
    }
}
