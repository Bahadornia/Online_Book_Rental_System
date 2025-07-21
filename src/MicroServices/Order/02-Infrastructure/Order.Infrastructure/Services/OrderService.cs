using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Dtos;
using Order.Domain.Enums;
using Order.Domain.IRepositories;
using Order.Domain.IServices;
using Order.Infrastructure.Services.Refit;

namespace Order.Infrastructure.Services;

internal class OrderService : IOrderService
{
    private readonly IInventoryApi _inventoryApi;
    private readonly IOrderRepository _orderRepository;
    private readonly IBookGrpcService _bookService;

    public OrderService(IInventoryApi inventoryApi, IOrderRepository orderRepository, IBookGrpcService bookService)
    {
        _inventoryApi = inventoryApi;
        _orderRepository = orderRepository;
        _bookService = bookService;
    }

    public Task<bool> CanUserRentBook(long userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<OrderListDto>> GetAll(CancellationToken ct)
    {
        var orders = await _orderRepository.GetAll(ct).ToListAsync(ct);
        var tasks = orders.Select(async (order) =>
        {
            var book = await _bookService.GetById(new GetBookRq { Id = order.BookId.Value }, ct);
            var rs = new OrderListDto
             (order.Id.Value,
              order.BookId,
              "",
              book.Title,
              book.ISBN,
              order.ReturnDate.Value ,
              order.BorrowDate,
              4,
              OrderStatus.OverDue);
            return rs;
        });
        var result = await Task.WhenAll(tasks);
        return result;
    }

    public async Task<bool> IsBookAvailable(long bookId, CancellationToken ct)
    {
        return await _inventoryApi.IsBookAvailable(bookId);
    }
}
