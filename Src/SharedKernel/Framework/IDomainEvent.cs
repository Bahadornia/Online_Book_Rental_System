namespace Framework;

public interface IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public string EventType => GetType().AssemblyQualifiedName!;
    public DateTime OccuredAt => DateTime.UtcNow;
}