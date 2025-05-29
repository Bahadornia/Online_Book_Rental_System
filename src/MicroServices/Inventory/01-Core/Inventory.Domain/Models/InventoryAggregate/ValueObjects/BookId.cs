
namespace Inventory.Domain.Models.InventoryAggregate.ValueObjects;

public record BookId
{
    public long Value { get; set; }

    private BookId(long value)
    {
        Value = value;
    }

    public static BookId Create(long value)
    {
        Validate(value);
        return new BookId(value);
    }

    public static implicit operator long(BookId id)
    {
        return id.Value;
    }

    public static implicit operator BookId(long value)
    {
        return Create(value);
    }
    private static void Validate(long value)
    {
        if(value <= 0)
        {
            throw new Exception("The value of identifier can not be zero or negative!");
        }
    }
}
