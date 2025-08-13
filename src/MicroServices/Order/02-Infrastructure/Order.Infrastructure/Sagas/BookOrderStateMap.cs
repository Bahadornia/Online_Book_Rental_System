using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Infrastructure.Sagas.BookOrderSagas;

namespace Order.Infrastructure.Sagas;

internal class BookOrderStateMap: SagaClassMap<BookOrderSagaState>
{
    protected override void Configure(EntityTypeBuilder<BookOrderSagaState> entity, ModelBuilder model)
    {
        entity.Property(x => x.BookId).IsRequired();
        entity.Property(x => x.UserId).IsRequired();
        entity.Property(x => x.CreatedAt).IsRequired();
        entity.Property(x => x.CurrentState).IsRequired();
    }
}
