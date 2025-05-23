using Framework;
using Rental.Domain.Models.RentalAggregate.Enums;
using Rental.Domain.Models.RentalAggregate.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rental.Domain.Models.RentalAggregate.Entities;

public class RentalHistory: Entity<RentalHistroyId>
{
    public RentalStatus Status { get; set; }
    public string? Description { get; set; }
    public RentalId RentalId { get; set; } = default!;
    public BookRental Rental { get; set; } = default!;
}
