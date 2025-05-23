using Rental.Domain.Models.RentalAggregate.Entities;

namespace Rental.Domain.IRepositories;

public interface IRentalRepository
{
    Task AddBookRental(BookRental bookRental, CancellationToken ct);
}
