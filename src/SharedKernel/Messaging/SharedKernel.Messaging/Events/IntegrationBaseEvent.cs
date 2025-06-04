namespace SharedKernel.Messaging.Events
{
    public class IntegrationBaseEvent
    {
        public long EventId { get; set; }
        public DateTime OccuredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
