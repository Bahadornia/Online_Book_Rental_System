using Rental.Domain.Dtos;
using Rental.Domain.IRepositories;
using Rental.Domain.Models.RentalAggregate.Entities;
using Rental.Infastructure.Data;

namespace Rental.Infastructure.Repositories;

internal class RentalRepository : IRentalRepository
{
    private readonly RentalDbContext _dbContext;


    public RentalRepository(RentalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddBookRental(BookRental rental, CancellationToken ct)
    {
        _dbContext.BookRentals.Add(rental);
        await _dbContext.SaveChangesAsync(ct);
    }
}
