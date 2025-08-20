namespace Order.Domain.Dtos;

public class OverDueDatedOrdersDto
{
    public long OrderId { get; init; }
    public long BookId { get; init; }
    public string UserId { get; init; }
    public DateTime DueDate { get; init; }
    public string? BookTitle { get; init; }
    public string? FullNam { get; init; }
};
