namespace Rental.Domain.IServices;

public interface IRentalService
{
    Task<bool> CanUserRentBook(long userId, CancellationToken ct);
    Task<bool> IsBookAvailable(long bookId, CancellationToken ct);
}
