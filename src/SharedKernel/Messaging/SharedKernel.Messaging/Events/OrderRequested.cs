namespace SharedKernel.Messaging.Events
{
    public sealed class OrderRequested : IntegrationBaseEvent
    {
        public string UserId { get; set; }
        public long BookId { get; set; }
    }
}
