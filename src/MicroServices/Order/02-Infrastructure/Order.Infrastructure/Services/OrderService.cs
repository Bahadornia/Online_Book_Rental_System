using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Responses;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Dtos;
using Order.Domain.IRepositories;
using Order.Domain.IServices;
using Order.Infrastructure.Data;
using Order.Infrastructure.Services.Refit;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;
using System.Text.Json;

namespace Order.Infrastructure.Services;

internal class OrderService : IOrderService
{
    private readonly IInventoryApi _inventoryApi;
    private readonly IOrderRepository _orderRepository;
    private readonly IBookGrpcService _bookService;
    private readonly IIntegrationEventPublisher _publisher;
    private readonly OrderDbContext _dbContext;

    public OrderService(IInventoryApi inventoryApi, IOrderRepository orderRepository, IBookGrpcService bookService, IIntegrationEventPublisher publisher, OrderDbContext dbContext)
    {
        _inventoryApi = inventoryApi;
        _orderRepository = orderRepository;
        _bookService = bookService;
        _publisher = publisher;
        _dbContext = dbContext;
    }

    public Task<bool> CanUserRentBook(string UserId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<OrderListDto>> GetAll(CancellationToken ct)
    {
        var orders = await _orderRepository.GetAll(ct);
        var bookIds = orders.Select(order => order.BookId.Value).Distinct();
        var books = await _bookService.GetBooksByIds(bookIds, ct);
        var bookDict = books.Where(book => book is not null).ToDictionary(b => b.Id);
        if (bookDict.Count > 0)
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

    async Task IOrderService.CheckOverDueDateOrders(CancellationToken ct)
    {
        IReadOnlyCollection<OverDueDatedOrdersDto> result = [];
        var projectedOverDueDatedOrders = (await _orderRepository.GetOverDueDatedOrders(ct)).Select(order =>
        new OverDueDatedOrdersDto
        {
            BookId = order.BookId,
            OrderId = order.Id,
            DueDate = order.DueDate,
            UserId = order.UserId,
        });

        if (projectedOverDueDatedOrders.Count() > 0)
        {
            var bookIds = projectedOverDueDatedOrders.Select(item => item.BookId);
            var userId = projectedOverDueDatedOrders.Select(item => item.UserId);

            var books = await _bookService.GetBooksByIds(bookIds, ct);
            var bookDict = books.Where(book => book is not null).ToDictionary(b => b.Id);

            result = projectedOverDueDatedOrders.Select(order => new OverDueDatedOrdersDto
            {
                BookId = order.BookId,
                OrderId = order.OrderId,
                BookTitle = "",
                DueDate = order.DueDate,
                FullNam = "",
                UserId = order.UserId
            }).ToList().AsReadOnly();
        }
        ;

        var eventData = new OrdersOverDueDatedIntegrationEvent
        {
            Data = JsonSerializer.Serialize(result)
        };
        try
        {
            await _publisher.Publish<OrdersOverDueDatedIntegrationEvent>(eventData, ct);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
