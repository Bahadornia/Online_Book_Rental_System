using Refit;

namespace Order.Infrastructure.Services.Refit;

interface IInventoryApi
{
    [Get("/{bookId}/avialable")]
    Task<bool> IsBookAvailable(long bookId);
}
