using Framework;
using Microsoft.Extensions.Logging;
using Rental.Domain.Models.RentalAggregate.Enums;
using Rental.Domain.Models.RentalAggregate.Events;
using Rental.Domain.Models.RentalAggregate.ValueObjects;

namespace Rental.Domain.Models.RentalAggregate.Entities;

public class BookRental : AggregateRoot<RentalId>
{

    private readonly List<RentalHistory> _history = [];
    public IReadOnlyCollection<RentalHistory> Histories => _history.AsReadOnly();

    public BookId BookId { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public DateTime BorrowDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public DateTime DueDate => BorrowDate.AddDays(14);
    public TimeSpan OverDueDate
    {
        get
        {
            if (ReturnDate.HasValue)

                return ReturnDate.Value.Subtract(DueDate);
            return default;
        }
    }
    public string? Description { get; set; }
    public RentalStatus Status { get; private set; }
    public bool IsExtended { get; private set; }

    private BookRental() { }
    public static BookRental Create(RentalId id, UserId userId, BookId bookId, DateTime borrowDate)
    {
        var rental = new BookRental
        {
            Id = id,
            BookId = bookId,
            UserId = userId,
            BorrowDate = borrowDate,
            Status = RentalStatus.Borrowed,
        };

        rental.Emit(new RentalAddedEvent(rental));
        return rental;
    }

    public void Update(long bookId, DateTime borrowDate, DateTime returnDate, RentalStatus status, bool isExtended)
    {
        BookId = bookId;
        BorrowDate = borrowDate;
        ReturnDate = returnDate;
        Status = status;
        IsExtended = isExtended;

        Emit(new RentalUpdatedEvent(this));
    }

    protected override void ValidateInvariants() 
    {
        if (BorrowDate > DateTime.UtcNow) throw new Exception("Borrow Date can not be in the future.");
        if (DueDate <= BorrowDate) throw new Exception("DueDate must be after BorrowDate");
        if (ReturnDate.HasValue) throw new Exception("Rental is already returned");
        if (ReturnDate < BorrowDate) throw new Exception("ReturnDate cannot be before BorrowDate");
        if (IsExtended) throw new Exception("Rental period has already been extended");
        return;
    }

    public void AddRentalHistory(RentalHistory history)
    {
        _history.Add(history);
    }

    
    public void SetStatus(RentalStatus status)
    {
        Status = status;
        var statusChangedEvent = new RentalUpdatedEvent(this);
    }

}
