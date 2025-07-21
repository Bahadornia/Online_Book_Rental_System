using ProtoBuf.Grpc;
using Order.API.Grpc.Client.Logics;
using Order.API.Grpc.Client.Requests;
using MediatR;
using Order.ApplicationServices.Commands;
using Order.API.Grpc.Client.Responses;
using Order.ApplicationServices.Queries;
using MapsterMapper;

namespace Order.API.Grpc.Services;

public class OrderGrpcService : IOrderGrpcService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetOrderRs>> GetAll(CallContext context)
    {
        var query = new GetAllOrdersQuery();
        var rs = await _mediator.Send(query, context.CancellationToken);
        return _mapper.Map<IReadOnlyCollection<GetOrderRs>>(rs);
    }

    public async Task RentBook(OrderBookRq rq, CallContext context = default)
    {
        var command = new AddOrderBookCommand(rq.BookId, rq.UserId, rq.BorrowDate);
       
        await _mediator.Send(command, context.CancellationToken);
    }
}
