namespace SharedKernel.Messaging.Events
{
    public abstract class IntegrationBaseEvent
    {
        public long EventId { get; set; }
        public DateTime OccuredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
