using Framework.Extensions;
using Website.Enum;

namespace Website.Dtos;

public class OrderDto
{
    public string OrderId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string BookId { get; set; } = default!;
    public string BookTitle { get; set; } = default!;
    public string ISBN { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int NumberOfExtending { get; set; }
    public OrderStatus Status { get; set; }
    public string StatusString => Status.GetDisplayName();
}
