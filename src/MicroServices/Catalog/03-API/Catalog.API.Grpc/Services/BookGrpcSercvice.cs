using Grpc.Core;
using Catalog.API.Grpc;
using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using ProtoBuf.Grpc;
using Catalog.ApplicationServices;
using Catalog.ApplicationServices.Commands;
using MediatR;
using MapsterMapper;
using Catalog.ApplicationServices.Queries;
using Catalog.API.Grpc.Client.Responses;

namespace Catalog.API.Grpc.Services;

public class BookGrpcSercvice : IBookGrpcService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BookGrpcSercvice(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task AddBook(AddBookRq rq, CallContext callContext)
    {

        var command = new AddBookCommand(rq.Title, rq.Author, rq.Publisher, rq.Category, rq.ISBN, rq.Description, rq.ContentType, rq.AvailableCopies, rq.Image);
        await _mediator.Send(command, callContext.CancellationToken);
    }

    public async Task<IReadOnlyCollection<GetBookRs>> GetAllBooks(CallContext callContext = default)
    {
        var query = new GetAllBookQuery();
        var rs = await _mediator.Send(query, callContext.CancellationToken);
        var result = _mapper.Map<IReadOnlyCollection<GetBookRs>>(rs);
        return result;
    }

    public async Task<GetBookImageRs> GetBookImage(GetBookImageRq rq, CallContext callContext)
    {
        var query = new GetBookImgeQuery(rq.FileName);
        var url = await _mediator.Send(query, callContext.CancellationToken);
        return new GetBookImageRs { Url = url };
    }
}
