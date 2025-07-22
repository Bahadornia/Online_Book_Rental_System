using Framework.Domain;
using Order.Domain.Enums;
using Order.Domain.Models.OrderAggregate.ValueObjects;

namespace Order.Domain.Models.OrderAggregate.Entities;

public class OrderHistory : Entity<OrderHistroyId>
{
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public OrderId OrderId { get; set; } = default!;
    public BookOrder Order { get; set; } = default!;
}
