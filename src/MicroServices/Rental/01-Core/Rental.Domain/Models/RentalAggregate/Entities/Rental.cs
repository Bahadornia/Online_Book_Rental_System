using Framework;
using Rental.Domain.Models.RentalAggregate.Enums;
using Rental.Domain.Models.RentalAggregate.ValueObjects;

namespace Rental.Domain.Models.RentalAggregate.Entities;

public class Rental: AggregateRoot<RentalId>
{
    public long BookId { get; set; }
    public long UserId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime DueDate { get; set; }
    public RentalStatus Status { get; set; }
    public int ExtensionNumber { get; set; }
    public IEnumerable<RentalHistroy> BookRentalHistroy { get; set; } = [];
}
