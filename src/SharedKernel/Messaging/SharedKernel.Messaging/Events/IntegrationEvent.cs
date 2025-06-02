namespace SharedKernel.Messaging.Events
{
    public interface IIntegrationEvent
    {
        public long EventId { get; set; }
        public DateTime OccuredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
