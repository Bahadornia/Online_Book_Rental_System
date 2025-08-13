using MassTransit;
using Order.Infrastructure.Sagas.BookOrderSagas;
using SharedKernel.Messaging.Events;


public class BookOrderSaga: MassTransitStateMachine<BookOrderSagaState>
{
    public State BookReserved { get; private set; }
    public State PaymentProcessed { get; private set; }
    public State Failed { get; private set; }

    public Event<OrderRequested> OrderRequestedEvent { get; private set; }
    public Event<BookReserved> BookReservedEvent { get; private set; }
    public Event<PaymentCompleted> PaymentCompletedEvent { get; private set; }
    public Event<OrderFailed> OrderFailedEvent { get; private set; }

    public BookOrderSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderRequestedEvent, x => x.CorrelateById(context => context.Message.CorrelationId)); 
        Event(() => BookReservedEvent, x => x.CorrelateById(context => context.Message.CorrelationId)); 
        Event(() => PaymentCompletedEvent, x => x.CorrelateById(context => context.Message.CorrelationId)); 
        Event(() => OrderFailedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(OrderRequestedEvent)
                .Then(context =>
                {
                    context.Saga.CorrelationId = context.Message.CorrelationId;
                    context.Saga.BookId = context.Message.BookId;
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .Publish(context => new ReserveBookCommand
                {
                    CorrelationId = context.Message.CorrelationId,
                    BookId = context.Saga.BookId
                })
                .TransitionTo(BookReserved));
        During(BookReserved,
            When(BookReservedEvent)
                .Publish(context => new ChargePaymentCommand
                {
                    CorrelationId = context.Message.CorrelationId,
                    UserId = context.Saga.UserId
                })
                .TransitionTo(PaymentProcessed),
        When(OrderFailedEvent)
            .TransitionTo(Failed)
            .Finalize());

        During(PaymentProcessed,
            When(PaymentCompletedEvent)
            .Finalize());
            
            
            SetCompletedWhenFinalized();
    }
    public State Available { get; private set; }
    public Event<BookRentedIntegrationEvent> BookRented { get; private set; }
}
