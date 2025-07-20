namespace Order.Domain.IServices;

public interface IOrderService
{
    Task<bool> CanUserRentBook(long userId, CancellationToken ct);
    Task<bool> IsBookAvailable(long bookId, CancellationToken ct);
}
