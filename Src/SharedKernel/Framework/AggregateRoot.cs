namespace Framework;

public abstract class AggregateRoot<T>: Entity<T> where T : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];
    protected IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvents(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
    protected IReadOnlyList<IDomainEvent> ClearDomainEvents()
    {
        var dequedEvents = _domainEvents;
        _domainEvents.Clear();
        return dequedEvents;
    } 
}
