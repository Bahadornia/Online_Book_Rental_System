namespace Framework;

public abstract class AggregateRoot<T>: Entity<T> where T : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];
    protected IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddEvents(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
    protected IReadOnlyList<IDomainEvent> ClearEvents()
    {
        var dequedEvents = _domainEvents;
        _domainEvents.Clear();
        return dequedEvents;
    } 
}
