namespace Rental.Domain.Models.RentalAggregate.Enums;

public enum RentalStatus
{
    Borrowed=0,
    Returned=10,
    OverDue = 20,
    Extended=30
}
