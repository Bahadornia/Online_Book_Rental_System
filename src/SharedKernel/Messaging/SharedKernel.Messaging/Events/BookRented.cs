namespace SharedKernel.Messaging.Events;

public class BookRented: IIntegrationEvent
{
    public long EventId { get; set; }
    public long BookId { get; set; }
    public DateTime BorrowDate { get; set; }
}
