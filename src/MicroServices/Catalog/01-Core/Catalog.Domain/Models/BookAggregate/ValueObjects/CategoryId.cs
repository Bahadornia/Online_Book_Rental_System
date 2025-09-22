
using Framework.Exceptions;

namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public readonly struct CategoryId
{
    public long Value { get; init; }

    private CategoryId(long categoryId) {
    
        Value = categoryId;
    }    

    public static CategoryId Create(long categoryId)
    {
        Validate(categoryId);
        return new CategoryId(categoryId);
    }

    private static void Validate(long categoryId)
    {
        if (categoryId <= 0) 
        {
            throw new InvalidIdentifierException(nameof(CategoryId));
        }
    }

    public static implicit operator CategoryId(long categoryId) {

        return Create(categoryId);
    }

    public static implicit operator long(CategoryId categoryId) { 
    
    return categoryId.Value;
    }

}
