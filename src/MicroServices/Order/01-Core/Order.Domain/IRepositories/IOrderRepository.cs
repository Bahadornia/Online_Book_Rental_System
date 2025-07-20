using Order.Domain.Models.RentalAggregate.Entities;

namespace Order.Domain.IRepositories;

public interface IOrderRepository
{
    void AddBookOrder(BookOrder bookRental, CancellationToken ct);
}
