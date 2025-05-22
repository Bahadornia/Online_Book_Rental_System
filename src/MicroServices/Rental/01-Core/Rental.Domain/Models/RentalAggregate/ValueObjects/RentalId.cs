using Microsoft.AspNetCore.Mvc;

namespace Rental.Domain.Models.RentalAggregate.ValueObjects;

public record RentalId
{
    private readonly long _value;
    public long Value { get; set; }

    private RentalId()
    {
        Value = _value;
    }

    public static RentalId Create(long value)
    {
        Validate(value);
        return new RentalId();
    }

    public static implicit operator long(RentalId Id)
    {
        return Id.Value;
    }
    public static implicit operator RentalId(long value)
    {
        return Create(value);
    }
    private static void Validate(long value)
    {
        if(value < 0)
        {
            throw new Exception("The value of identifier can not be negative!");
        }
    }
}
