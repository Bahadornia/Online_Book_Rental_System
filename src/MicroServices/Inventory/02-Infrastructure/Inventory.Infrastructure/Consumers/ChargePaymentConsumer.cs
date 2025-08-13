using MassTransit;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;

namespace Inventory.Infrastructure.Consumers;

public sealed class ChargePaymentConsumer : IConsumer<ChargePaymentCommand>
{
    private readonly IIntegrationEventPublisher _publisher;

    public ChargePaymentConsumer(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Consume(ConsumeContext<ChargePaymentCommand> context)
    {
        await _publisher.Publish<PaymentCompletedEvent>(new PaymentCompletedEvent { CorrelationId = context.Message.CorrelationId, PaymentId = 1111111111111 }, context.CancellationToken);
    }
}
