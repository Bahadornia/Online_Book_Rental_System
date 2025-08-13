namespace SharedKernel.Messaging.Events;

public sealed class BookReserved: IntegrationBaseEvent
{
    public long BookId { get; set; }
}
