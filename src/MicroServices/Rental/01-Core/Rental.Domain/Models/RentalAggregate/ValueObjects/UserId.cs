namespace Rental.Domain.Models.RentalAggregate.ValueObjects;

public record UserId
{
    public long Value { get; set; }

    private UserId(long value)
    {
        Value = value;
    }

    public static UserId Create(long value)
    {
        Validate(value);
        return new UserId(value);
    }

    public static implicit operator long(UserId Id)
    {
        return Id.Value;
    }
    public static implicit operator UserId(long value)
    {
        return Create(value);
    }
    private static void Validate(long value)
    {
        if (value <= 0)
        {
            throw new Exception("The value of identifier can not be zero or negative!");
        }
    }
}
