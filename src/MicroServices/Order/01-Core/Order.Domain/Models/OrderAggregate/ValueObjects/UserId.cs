namespace Order.Domain.Models.OrderAggregate.ValueObjects;

public record UserId
{
    public string Value { get; set; }

    private UserId(string value)
    {
        Value = value;
    }

    public static UserId Create(string value)
    {
        Validate(value);
        return new UserId(value);
    }

    public static implicit operator string(UserId Id)
    {
        return Id.Value;
    }
    public static implicit operator UserId(string value)
    {
        return Create(value);
    }
    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new Exception("The value of identifier can not be null!");
        }
    }
}
