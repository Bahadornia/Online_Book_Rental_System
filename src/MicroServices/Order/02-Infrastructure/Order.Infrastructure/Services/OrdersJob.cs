using Hangfire;
using Order.Domain.IServices;

namespace Order.Infrastructure.Services;
internal class OrdersJob
{
    public const string ORDERS_JOBID= "OrdersJob";
    public const string ORDERS_JOB_CRON = "59 10 * * 7";

    private readonly IOrderService _orderService;

    public OrdersJob(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public void Execute(CancellationToken ct)
    {
        BackgroundJob.Enqueue(() => CheckOverDueDateOrders(ct));
    }

    public async Task CheckOverDueDateOrders(CancellationToken ct)
    {
        await _orderService.CheckOverDueDateOrders(ct);
    }
}
