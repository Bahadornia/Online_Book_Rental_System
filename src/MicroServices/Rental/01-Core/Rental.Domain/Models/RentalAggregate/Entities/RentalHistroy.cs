using Framework;
using Rental.Domain.Models.RentalAggregate.Enums;
using Rental.Domain.Models.RentalAggregate.ValueObjects;

namespace Rental.Domain.Models.RentalAggregate.Entities;

public class RentalHistroy: Entity<RentalHistroyId>
{
    public required RentalId RentalId { get; set; }
    public RentalStatus RentalStatus { get; set; }
    public string? Description { get; set; }
}
