using MassTransit;
using SharedKernel.Messaging.Events;

namespace Notification.Infrastructure.Consumers
{
    public class BookAddedConsumer : IConsumer<BookAddedIntegrationEvent>
    {


        public async Task Consume(ConsumeContext<BookAddedIntegrationEvent> context)
        {
            await Task.CompletedTask;
        }
    }

    public class BookAddedFualtConsumer : IConsumer<Fault<BookAddedIntegrationEvent>>
    {
        public Task Consume(ConsumeContext<Fault<BookAddedIntegrationEvent>> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}
