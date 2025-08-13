namespace SharedKernel.Messaging.Events;

public sealed class PaymentCompleted : IntegrationBaseEvent
{
    public Guid CorrelationId { get; set; }
}
