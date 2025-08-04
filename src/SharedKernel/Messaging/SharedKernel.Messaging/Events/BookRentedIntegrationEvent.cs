namespace SharedKernel.Messaging.Events;

public class BookRentedIntegrationEvent: IntegrationBaseEvent
{
    public long BookId { get; set; }
    public DateTime BorrowDate { get; set; }
}
