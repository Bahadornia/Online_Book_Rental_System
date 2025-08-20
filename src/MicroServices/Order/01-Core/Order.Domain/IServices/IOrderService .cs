using Order.Domain.Dtos;

namespace Order.Domain.IServices;

public interface IOrderService
{
    Task<IReadOnlyCollection<OrderListDto>> GetAll(CancellationToken ct);
    Task<bool> CanUserRentBook(string UserId, CancellationToken ct);
    Task<bool> IsBookAvailable(long bookId, CancellationToken ct);
    Task CheckOverDueDateOrders(CancellationToken ct);
}
