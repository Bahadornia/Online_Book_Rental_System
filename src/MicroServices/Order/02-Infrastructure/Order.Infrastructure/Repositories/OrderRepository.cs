using Order.Domain.Dtos;
using Order.Domain.IRepositories;
using Order.Domain.Models.RentalAggregate.Entities;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

internal class RentalRepository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;


    public RentalRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddBookOrder(BookOrder rental, CancellationToken ct)
    {
        _dbContext.BookRentals.Add(rental);
    }
}
