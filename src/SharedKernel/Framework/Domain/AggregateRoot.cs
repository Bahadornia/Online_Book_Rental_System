namespace Framework.Domain;

public abstract class AggregateRoot<T>: Entity<T>, IAggregateRoot where T : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvents(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    protected abstract void ValidateInvariants();
    public void Emit(IDomainEvent @event)
    {
        ValidateInvariants();
        AddDomainEvents(@event);
    }
   
    public IReadOnlyList<IDomainEvent> ClearDomainEvents()
    {
        IDomainEvent[] dequedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequedEvents;
    }
}
