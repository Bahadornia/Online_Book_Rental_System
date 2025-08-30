using Order.Domain.Models.OrderAggregate.Entities;

namespace Order.Domain.IRepositories;

public interface IOrderRepository
{
    void AddBookOrder(BookOrder bookRental, CancellationToken ct);
    Task<IEnumerable<BookOrder>> GetAll(CancellationToken ct);
    Task<IEnumerable<BookOrder>> GetOverDueDatedOrders(CancellationToken ct);
}
