using Rental.Domain.Models.RentalAggregate.Entities;

namespace Rental.Domain.IRepositories;

public interface IRentalRepository
{
    void AddBookRental(BookRental bookRental, CancellationToken ct);
}
