namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public record Publisher
{
    public string Name { get; private set; } = default!;

    private Publisher(string value)
    {

        Name = value;
    }

    public static Publisher Create(string value)
    {
        Validate(value);
        return new Publisher(value);
    }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new Exception("The publisher is required");
        }
    }
};
