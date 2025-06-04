namespace SharedKernel.Messaging.Events;

public class BookRented: IntegrationBaseEvent
{
    public long EventId { get; set; }
    public long BookId { get; set; }
    public DateTime BorrowDate { get; set; }
}
