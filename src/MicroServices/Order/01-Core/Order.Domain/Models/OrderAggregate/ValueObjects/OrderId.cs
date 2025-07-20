using Microsoft.AspNetCore.Mvc;

namespace Order.Domain.Models.RentalAggregate.ValueObjects;

public record OrderId
{
    public long Value { get; set; }

    private OrderId(long value)
    {
        Value = value;
    }

    public static OrderId Create(long value)
    {
        Validate(value);
        return new OrderId(value);
    }

    public static implicit operator long(OrderId Id)
    {
        return Id.Value;
    }
    public static implicit operator OrderId(long value)
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
