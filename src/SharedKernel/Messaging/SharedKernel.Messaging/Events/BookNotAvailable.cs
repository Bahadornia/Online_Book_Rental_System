namespace SharedKernel.Messaging.Events;

public class BookNotAvailable : IntegrationEvent
{
    public long EventId { get; set;}
    public long BookId { get; set; }
}

