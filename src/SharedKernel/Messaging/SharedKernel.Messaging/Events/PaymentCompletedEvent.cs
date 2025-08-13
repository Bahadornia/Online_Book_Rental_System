namespace SharedKernel.Messaging.Events;

public sealed class PaymentCompletedEvent: IntegrationBaseEvent
{
    public long PaymentId { get; set; }
}
