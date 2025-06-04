namespace Framework.Domain;

public interface IDomainEventPublisher
{
    void Publish(IReadOnlyList<IDomainEvent> @event);
}
