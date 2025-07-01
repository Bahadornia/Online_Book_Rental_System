using Rental.Domain.IServices;
using Rental.Infrastructure.Services.Refit;

namespace Rental.Infrastructure.Services;

internal class RentalService : IRentalService
{
    private readonly IInventoryApi _inventoryApi;

    public RentalService(IInventoryApi inventoryApi)
    {
        _inventoryApi = inventoryApi;
    }

    public Task<bool> CanUserRentBook(long userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsBookAvailable(long bookId, CancellationToken ct)
    {
         return await _inventoryApi.IsBookAvailable(bookId);
    }
}
