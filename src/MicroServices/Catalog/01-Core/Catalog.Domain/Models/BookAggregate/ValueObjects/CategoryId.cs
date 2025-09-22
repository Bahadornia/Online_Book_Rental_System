
using Framework.Exceptions;

namespace Catalog.Domain.Models.BookAggregate.ValueObjects;

public readonly struct CategoryId
{
    public int Value { get; init; }

    private CategoryId(int categoryId) {
    
        Value = categoryId;
    }    

    public static CategoryId Create(int categoryId)
    {
        Validate(categoryId);
        return new CategoryId(categoryId);
    }

    private static void Validate(int categoryId)
    {
        if (categoryId <= 0) 
        {
            throw new InvalidIdentifierException(nameof(CategoryId));
        }
    }

    public static implicit operator CategoryId(int categoryId) {

        return Create(categoryId);
    }

    public static implicit operator long(CategoryId categoryId) { 
    
    return categoryId.Value;
    }

}
