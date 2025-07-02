using Rental.Domain.Dtos;
using Rental.Domain.IRepositories;
using Rental.Domain.Models.RentalAggregate.Entities;
using Rental.Infrastructure.Data;

namespace Rental.Infrastructure.Repositories;

internal class RentalRepository : IRentalRepository
{
    private readonly RentalDbContext _dbContext;


    public RentalRepository(RentalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddBookRental(BookRental rental, CancellationToken ct)
    {
        _dbContext.BookRentals.Add(rental);
    }
}
