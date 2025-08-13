namespace SharedKernel.Messaging.Events;

public sealed class ChargePaymentCommand: IntegrationBaseEvent
{
    public long UserId { get; set; }
}
