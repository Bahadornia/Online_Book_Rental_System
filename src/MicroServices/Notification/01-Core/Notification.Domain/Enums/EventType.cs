namespace Notification.Domain.Enums;
public enum EventType
{
    NONE = 0,
    BOOK_RESERVED = 10,
    ORDER_OVER_DUEDATE = 20,
    ORDER_CANCELLED = 21, 
    ORDER_FAILED = 22,
    USER_REGISTERED = 30,
}
