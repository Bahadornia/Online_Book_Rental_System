using MediatR;

namespace Framework;

public interface IDomainEvent: INotification
{
    public Guid EventId => Guid.NewGuid();
    public string EventType => GetType().AssemblyQualifiedName!;
    public DateTime OccuredAt => DateTime.UtcNow;
}