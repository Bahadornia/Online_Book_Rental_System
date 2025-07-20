namespace Order.Domain.Models.RentalAggregate.ValueObjects;

public record OrderHistroyId
{
    public long Value { get; set; }

    private OrderHistroyId(long value)
    {
        Value = value;
    }

    public static OrderHistroyId Create(long value)
    {
        Validate(value);
        return new OrderHistroyId(value);
    }

    public static implicit operator long(OrderHistroyId Id)
    {
        return Id.Value;
    }
    public static implicit operator OrderHistroyId(long value)
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
