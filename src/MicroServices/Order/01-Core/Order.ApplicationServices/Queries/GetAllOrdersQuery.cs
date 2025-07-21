using Framework.CQRS;
using Order.Domain.Dtos;

namespace Order.ApplicationServices.Queries;

public record GetAllOrdersQuery(): IQuery<IReadOnlyCollection<OrderListDto>>;
