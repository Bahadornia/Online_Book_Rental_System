using Framework;
using Rental.Domain.Models.RentalAggregate.Entities;

namespace Rental.Domain.Models.RentalAggregate.Events;

public record UpdateRentalEvent(BookRental rental): IDomainEvent;
