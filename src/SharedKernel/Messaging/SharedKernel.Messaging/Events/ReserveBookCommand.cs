namespace SharedKernel.Messaging.Events;

public sealed class ReserveBookCommand
{
    public Guid CorrelationId { get; set; }
    public long BookId { get; set; }
}
