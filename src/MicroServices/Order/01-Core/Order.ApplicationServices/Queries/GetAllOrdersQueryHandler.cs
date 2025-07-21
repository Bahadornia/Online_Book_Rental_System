using Framework.CQRS;
using Order.Domain.Dtos;
using Order.Domain.IServices;

namespace Order.ApplicationServices.Queries;

public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, IReadOnlyCollection<OrderListDto>>
{
    private readonly IOrderService _orderSerivce;

    public GetAllOrdersQueryHandler(IOrderService orderSerivce)
    {
        _orderSerivce = orderSerivce;
    }

    public async Task<IReadOnlyCollection<OrderListDto>> Handle(GetAllOrdersQuery request, CancellationToken ct)
    {
        var rs = await _orderSerivce.GetAll(ct);
        return rs;
    }
}
