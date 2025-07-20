namespace Website.Dtos;

public class OrderDto
{
    public string OrderId { get; set; } = default!;
    public string BookId { get; set; } = default!;
    public string BookTitle { get; set; } = default!;
    public string ISBN { get; set; } = default!;
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int NumberOfExtending { get; set; }
    public OrderStatus Status { get; set; }
}
