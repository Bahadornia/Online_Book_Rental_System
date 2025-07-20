namespace Order.Domain.Models.RentalAggregate.Enums;

public enum OrderStatus
{
    Borrowed=0,
    Returned=10,
    OverDue = 20,
    Extended=30
}
