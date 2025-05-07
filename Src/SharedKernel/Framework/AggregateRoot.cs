namespace Framework;

public abstract class AggregateRoot<T>: Entity<T> where T : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvents(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
    protected IReadOnlyList<IDomainEvent> ClearDomainEvents()
    {
        IDomainEvent[] dequedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequedEvents;
    } 
}
