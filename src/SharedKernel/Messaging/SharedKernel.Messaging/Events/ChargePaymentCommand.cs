namespace SharedKernel.Messaging.Events;

public sealed class ChargePaymentCommand: IntegrationBaseEvent
{
    public string UserId { get; set; }
}
