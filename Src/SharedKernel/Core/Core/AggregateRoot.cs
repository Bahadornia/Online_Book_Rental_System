namespace Core;

public abstract class AggregateRoot<T>: Entity<T> where T: struct
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddEvents(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
    public IReadOnlyList<IDomainEvent> ClearEvents()
    {
        var dequedEvents = _domainEvents;
        _domainEvents.Clear();
        return dequedEvents;
    } 
}
