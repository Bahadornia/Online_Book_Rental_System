namespace SharedKernel.Messaging
{
    public interface IIntegrationEvent
    {
        public Guid EventId => Guid.NewGuid();
        public DateTime OccuredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName!;

    }
}
