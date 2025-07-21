using Framework.Domain;
using Order.Domain.Models.RentalAggregate.Enums;
using Order.Domain.Models.RentalAggregate.Events;
using Order.Domain.Models.RentalAggregate.ValueObjects;

namespace Order.Domain.Models.RentalAggregate.Entities;

public class BookOrder : AggregateRoot<OrderId>
{

    private readonly List<OrderHistory> _history = [];
    public IReadOnlyCollection<OrderHistory> Histories => _history.AsReadOnly();

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
    public OrderStatus Status { get; private set; }
    public bool IsExtended { get; private set; }

    private BookOrder() { }
    public static BookOrder Create(OrderId id, UserId userId, BookId bookId, DateTime borrowDate)
    {
        var order = new BookOrder
        {
            Id = id,
            BookId = bookId,
            UserId = userId,
            BorrowDate = borrowDate,
            Status = OrderStatus.Borrowed,
        };

        order.Emit(new OrderAddedEvent(order));
        return order;
    }

    public void Update(long bookId, DateTime borrowDate, DateTime returnDate, OrderStatus status, bool isExtended)
    {
        BookId = bookId;
        BorrowDate = borrowDate;
        ReturnDate = returnDate;
        Status = status;
        IsExtended = isExtended;

        Emit(new OrderUpdatedEvent(this));
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

    public void AddOrderHistory(OrderHistory history)
    {
        _history.Add(history);
    }


    public void SetStatus(OrderStatus status)
    {
        Status = status;
        var statusChangedEvent = new OrderUpdatedEvent(this);
    }

}
