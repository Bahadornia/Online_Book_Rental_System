namespace SharedKernel.Messaging.Events;

public class BookNotAvailable : IntegrationBaseEvent
{
    public long EventId { get; set;}
    public long BookId { get; set; }
}

