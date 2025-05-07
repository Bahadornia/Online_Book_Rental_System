using Grpc.Core;
using Catalog.API.Grpc;
using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using ProtoBuf.Grpc;
using Catalog.ApplicationServices;
using Catalog.ApplicationServices.Commands;
using MediatR;
using MapsterMapper;

namespace Catalog.API.Grpc.Services;

public class BookGrpcSercvice : IBookGrpcService
{
    private readonly IMediator _mediator;

    public BookGrpcSercvice(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task AddBook(AddBookRq rq, CallContext callContext)
    {

        var command = new AddBookCommand(rq.Title, rq.Author, rq.PublisherId, rq.CategoryId, rq.ISBN, rq.Description, rq.Image);
        await _mediator.Send(command, callContext.CancellationToken);
    }
}
