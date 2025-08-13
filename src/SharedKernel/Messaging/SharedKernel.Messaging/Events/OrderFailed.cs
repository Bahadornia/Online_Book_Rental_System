namespace SharedKernel.Messaging.Events;

public sealed class OrderFailed: IntegrationBaseEvent
{
    public Guid CorrelationId { get; set; }
    public string? Reason { get; set; }
}
