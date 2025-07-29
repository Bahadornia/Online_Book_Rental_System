using Catalog.API.Grpc.Client.Logics;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Dtos;
using Order.Domain.IRepositories;
using Order.Domain.IServices;
using Order.Infrastructure.Services.Refit;

namespace Order.Infrastructure.Services;

internal class OrderService : IOrderService
{
    private readonly IInventoryApi _inventoryApi;
    private readonly IOrderRepository _orderRepository;
    private readonly IBookGrpcService _bookService;
    private readonly IMapper _mapper;

    public OrderService(IInventoryApi inventoryApi, IOrderRepository orderRepository, IBookGrpcService bookService, IMapper mapper)
    {
        _inventoryApi = inventoryApi;
        _orderRepository = orderRepository;
        _bookService = bookService;
        _mapper = mapper;
    }

    public Task<bool> CanUserRentBook(long userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<OrderListDto>> GetAll(CancellationToken ct)
    {
        var orders = await _orderRepository.GetAll(ct).ToListAsync(ct);
        var bookIds = orders.Select(order => order.BookId.Value).Distinct();
        var books = await _bookService.GetBooksByIds(bookIds, ct);
        var bookDict = books.Where(book => book is not null).ToDictionary(b => b.Id);
        if(bookDict.Count > 0)
        {
        var result = orders.Select(order =>
        {
            var book = bookDict[order.BookId.Value];
            var rs = new OrderListDto
             (order.Id,
              order.BookId,
              "",
              book.Title,
              book.ISBN,
              order.ReturnDate ?? order.BorrowDate.AddDays(14),
              order.BorrowDate,
              order.IsExtended ? 1 : 0,
              order.Status);
            return rs;
        });
        return result.ToList().AsReadOnly();

        }
        return Enumerable.Empty<OrderListDto>().ToList().AsReadOnly();
    }

    public async Task<bool> IsBookAvailable(long bookId, CancellationToken ct)
    {
        return await _inventoryApi.IsBookAvailable(bookId);
    }
}
