using MediatR;

namespace Framework.Domain;

public interface IDomainEvent: INotification
{
    public Guid EventId => Guid.NewGuid();
    public string EventType => GetType().AssemblyQualifiedName!;
    public DateTime OccuredAt => DateTime.UtcNow;
}