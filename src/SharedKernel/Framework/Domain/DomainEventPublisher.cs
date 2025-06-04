using MediatR;

namespace Framework.Domain;

internal class DomainEventPublisher : IDomainEventPublisher
{

    private readonly IPublisher _publisher;

    public DomainEventPublisher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public void Publish(IReadOnlyList<IDomainEvent> domainEvents)
    {
        foreach(var domainEvent in domainEvents)
        _publisher.Publish(domainEvent);
    }
}
