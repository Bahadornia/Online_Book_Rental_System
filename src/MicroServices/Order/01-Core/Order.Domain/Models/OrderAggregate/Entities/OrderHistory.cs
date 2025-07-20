using Framework.Domain;
using Order.Domain.Models.RentalAggregate.Enums;
using Order.Domain.Models.RentalAggregate.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Models.RentalAggregate.Entities;

public class OrderHistory: Entity<OrderHistroyId>
{
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public OrderId OrderId { get; set; } = default!;
    public BookOrder Rental { get; set; } = default!;
}
