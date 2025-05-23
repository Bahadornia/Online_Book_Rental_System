using ProtoBuf.Grpc;
using Rental.API.Grpc.Client.Logics;
using Rental.API.Grpc.Client.Requests;
using MediatR;
using Rental.ApplicationServices.Commands;

namespace Rental.API.Grpc.Services;

public class RentalGrpcService : IRentalGrpcService
{
    private readonly IMediator _mediator;

    public RentalGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task BorrowBook(BorrowBookRq rq, CallContext context = default)
    {
        var command = new AddRentalBookCommand(rq.BookId, rq.UserId, rq.BorrowDate);
       
        await _mediator.Send(command, context.CancellationToken);
    }
}
