
using Framework.Exceptions;

namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public readonly record struct PublisherId
{
    public int Value { get; init; }
    private PublisherId(int publisherId)
    {
        Value = publisherId;
    }

    public static PublisherId Create(int publisherId)
    {
        Validate(publisherId);
        return new PublisherId(publisherId);
    }

    private static void Validate(int publisherId)
    {
        if(publisherId <= 0)
        {
            throw new InvalidIdentifierException(nameof(publisherId));
        }
    }
    public static implicit operator PublisherId(int publisherId)
    {
        return Create(publisherId);
    }

    public static implicit operator long(PublisherId publisherId) 
    {
        return publisherId.Value;
    }

}
