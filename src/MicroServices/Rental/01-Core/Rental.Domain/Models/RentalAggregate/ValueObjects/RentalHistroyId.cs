namespace Rental.Domain.Models.RentalAggregate.ValueObjects;

public record RentalHistroyId
{
    private readonly long _value;
    public long Value { get; set; }

    private RentalHistroyId()
    {
        Value = _value;
    }

    public static RentalHistroyId Create(long value)
    {
        Validate(value);
        return new RentalHistroyId();
    }

    public static implicit operator long(RentalHistroyId Id)
    {
        return Id.Value;
    }
    public static implicit operator RentalHistroyId(long value)
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
