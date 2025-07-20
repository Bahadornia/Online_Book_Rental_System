using ProtoBuf.Grpc;
using Order.API.Grpc.Client.Logics;
using Order.API.Grpc.Client.Requests;
using MediatR;
using Order.ApplicationServices.Commands;

namespace Order.API.Grpc.Services;

public class RentalGrpcService : IRentalGrpcService
{
    private readonly IMediator _mediator;

    public RentalGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RentBook(RentBookRq rq, CallContext context = default)
    {
        var command = new AddOrderBookCommand(rq.BookId, rq.UserId, rq.BorrowDate);
       
        await _mediator.Send(command, context.CancellationToken);
    }
}
