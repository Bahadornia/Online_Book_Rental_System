using Refit;

namespace Rental.Infrastructure.Services.Refit;

interface IInventoryApi
{
    [Get("/{bookId}/avialable")]
    Task<bool> IsBookAvailable(long bookId);
}
