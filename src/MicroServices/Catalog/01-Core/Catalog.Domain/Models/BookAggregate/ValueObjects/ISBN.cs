namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public record ISBN
{
    public string Value { get; set; }
    private ISBN(string ISBN)
    {
        Value = ISBN;
    }

    public static ISBN Create(string ISBN)
    {

        ValidateIsbn(ISBN);
        return new ISBN(ISBN);
    }
    private static void ValidateIsbn(string ISBN)
    {
        if (string.IsNullOrWhiteSpace(ISBN) || ISBN.Length < 13) throw new Exception("Invalid ISBN");
    }

    public static implicit operator ISBN(string ISBN)
    {
        return Create(ISBN);
    }

    public static implicit operator string(ISBN ISBN)
    {
        return ISBN.Value;
    }
}
