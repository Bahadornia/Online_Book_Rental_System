namespace SharedKernel.Messaging.Events
{
    public sealed class OrderRequested : IntegrationBaseEvent
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
    }
}
