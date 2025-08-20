using MassTransit;

namespace Order.Infrastructure.Sagas.BookOrderSagas;

public class BookOrderSagaState: SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = default!;
    public string UserId { get; set; }
    public long BookId { get; set; }
    public DateTime? CreatedAt { get; set; }
}
