
using Framework.Exceptions;

namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public class PublisherId
{
    public long Value { get; private set; }
    private PublisherId(long publisherId)
    {
        Value = publisherId;
    }

    public static PublisherId Create(long publisherId)
    {
        Validate(publisherId);
        return new PublisherId(publisherId);
    }

    private static void Validate(long publisherId)
    {
        if(publisherId <= 0)
        {
            throw new InvalidIdentifierException(nameof(publisherId));
        }
    }
    public static implicit operator PublisherId(long publisherId)
    {
        return Create(publisherId);
    }

    public static implicit operator long(PublisherId publisherId) 
    {
        return publisherId.Value;
    }

}
