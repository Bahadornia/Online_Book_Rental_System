using Order.Domain.Enums;

namespace Order.Domain.Dtos;

public record OrderListDto
    (
    long OrderId,
    long BookId,
    string FullName,
    string BookTitle,
    string ISBN,
    DateTime RentDate,
    DateTime ReturnDate,
    int NumberOfExtending,
    OrderStatus Status
    );

