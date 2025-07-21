using Order.Domain.Dtos;

namespace Order.Domain.IServices;

public interface IOrderService
{
    Task<IReadOnlyCollection<OrderListDto>> GetAll(CancellationToken ct);
    Task<bool> CanUserRentBook(long userId, CancellationToken ct);
    Task<bool> IsBookAvailable(long bookId, CancellationToken ct);
}
