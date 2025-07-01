namespace SharedKernel.Messaging.Events;

public class BookDeletedIntegrationEvent : IntegrationBaseEvent
{
    public long BookId { get; set; }
}
