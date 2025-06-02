namespace SharedKernel.Messaging.Events;

public class BookNotAvailable : IIntegrationEvent
{
    public long EventId { get; set;}
    public long BookId { get; set; }
}

