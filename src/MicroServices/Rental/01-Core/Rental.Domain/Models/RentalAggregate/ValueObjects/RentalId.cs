using Microsoft.AspNetCore.Mvc;

namespace Rental.Domain.Models.RentalAggregate.ValueObjects;

public record RentalId
{
    public long Value { get; set; }

    private RentalId(long value)
    {
        Value = value;
    }

    public static RentalId Create(long value)
    {
        Validate(value);
        return new RentalId(value);
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
