using Framework.Domain;

namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public record Category
{
    public string Name { get; private set; } = default!;

    private Category(string value)
    {

        Name = value;
    }

    public static Category Create(string value)
    {
        Validate(value);
        return new Category(value);
    }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new Exception("The Category is required");
        }
    }
};
