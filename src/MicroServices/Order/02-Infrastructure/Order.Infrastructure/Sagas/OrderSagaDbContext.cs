using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Sagas.BookOrderSagas;
using MassTransit.EntityFrameworkCoreIntegration;

namespace Order.Infrastructure.Sagas;

public class OrderSagaDbContext : SagaDbContext
{
    public OrderSagaDbContext(DbContextOptions<OrderSagaDbContext> options) : base(options) { }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new BookOrderStateMap();
        }
    }


}
