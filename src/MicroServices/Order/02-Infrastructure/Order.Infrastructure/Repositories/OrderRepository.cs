using Microsoft.EntityFrameworkCore;
using Order.Domain.Dtos;
using Order.Domain.IRepositories;
using Order.Domain.Models.RentalAggregate.Entities;
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

    public IQueryable<BookOrder> GetAll(CancellationToken ct)
    {
        var bookOrders = _dbContext.BookOrders.Include(bookOrder => bookOrder.Histories);
        return bookOrders;
    }
}
