using Microsoft.EntityFrameworkCore;
using Order.Domain.IRepositories;
using Order.Domain.Models.OrderAggregate.Entities;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

internal class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;


    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddBookOrder(BookOrder rental, CancellationToken ct)
    {
        _dbContext.BookOrders.Add(rental);
    }

    public async Task<IEnumerable<BookOrder>> GetAll(CancellationToken ct)
    {
        var bookOrders = await _dbContext.BookOrders.Include(bookOrder => bookOrder.Histories).ToListAsync(); ;
        return bookOrders;
    }

    public async Task<IEnumerable<BookOrder>> GetOverDueDatedOrders(CancellationToken ct)
    {
        var overDueDated = await _dbContext.BookOrders.Where(order => order.DueDate < DateTime.Now.AddDays(1)).ToListAsync(ct);
        return overDueDated;
    }
}
