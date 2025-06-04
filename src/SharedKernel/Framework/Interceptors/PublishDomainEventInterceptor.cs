using Framework.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Framework.Interceptors
{
    public class PublishDomainEventInterceptor: SaveChangesInterceptor
    {
        private readonly IPublisher _publisher;

        public PublishDomainEventInterceptor(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if(eventData.Context is not null)
            {
                await PulbishDomainEvents(eventData.Context);
            }
            return result;
        }

        private async Task PulbishDomainEvents(DbContext context)
        {
            var domainEvents = context.ChangeTracker.Entries<IAggregateRoot>().Select(entry => entry.Entity).SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();

            foreach(var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
