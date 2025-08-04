namespace SharedKernel.Messaging.Events;

public class OrdersOverDueDatedIntegrationEvent: IntegrationBaseEvent
{
    public string? Data { get; set; }
}
