namespace Rental.Domain.Models.RentalAggregate.ValueObjects;

public record BookId
{
    private readonly long _value;
    public long Value { get; set; }

    private BookId()
    {
        Value = _value;
    }

    public static BookId Create(long value)
    {
        Validate(value);
        return new BookId();
    }

    public static implicit operator long(BookId Id)
    {
        return Id.Value;
    }
    public static implicit operator BookId(long value)
    {
        return Create(value);
    }
    private static void Validate(long value)
    {
        if (value < 0)
        {
            throw new Exception("The value of identifier can not be negative!");
        }
    }
}
