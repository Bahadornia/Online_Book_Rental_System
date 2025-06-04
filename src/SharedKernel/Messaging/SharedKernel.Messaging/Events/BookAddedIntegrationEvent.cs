namespace SharedKernel.Messaging.Events;

public class BookAddedIntegrationEvent: IntegrationBaseEvent
{
    public long BookId { get; set; }
    public int AvailableCopies { get; set; }
}
