using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Requests;
using Catalog.API.Grpc.Client.Responses;
using Catalog.ApplicationServices.Commands;
using Catalog.ApplicationServices.Queries;
using Catalog.Domain.Dtos;
using MapsterMapper;
using MediatR;
using ProtoBuf.Grpc;

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

        var command = new AddBookCommand(rq.Title, rq.Author,rq.PubliserName, rq.PublisherId,rq.CategoryName, rq.CategoryId, rq.ISBN, rq.Description, rq.ContentType, rq.AvailableCopies, rq.Image);
        await _mediator.Send(command, callContext.CancellationToken);
    }

    public async Task<GetAllBookRs> GetAllBooks(AgGridRequestRq rq, CallContext callContext)
    {
        var agGridRequestDto = _mapper.Map<AgGridRequestDto>(rq);
        var query = new GetAllBookQuery(agGridRequestDto);
        var rs = await _mediator.Send(query, callContext.CancellationToken);
        var result = new GetAllBookRs
        {
            Books = _mapper.Map<IReadOnlyCollection<GetBookRs>>(rs.Books),
            TotalCount = rs.TotalCount,
        };
        return result;
    }

    public async Task<GetBookImageRs> GetBookImage(GetBookImageRq rq, CallContext callContext)
    {
        var query = new GetBookImgeQuery(rq.FileName);
        var url = await _mediator.Send(query, callContext.CancellationToken);
        return new GetBookImageRs { Url = url };
    }

    public async Task<IReadOnlyCollection<GetBookRs>> GetBooksByIds(IEnumerable<long> ids, CallContext context)
    {
        var query = new GetBooksByIdsQuery(ids);
        var books = await _mediator.Send(query, context.CancellationToken);
        return _mapper.Map<IReadOnlyCollection<GetBookRs>>(books);
    }

    public async Task<GetBookRs> GetById(GetBookRq rq, CallContext context)
    {
        var command = new GetBookQuery(rq.Id);
        var book = await _mediator.Send(command, context.CancellationToken);
        return _mapper.Map<GetBookRs>(book);
    }

    public async Task<IReadOnlyCollection<GetBookRs>> SearchBook(BookFilterRq rq, CallContext callContext)
    {
        var bookFilterDto = _mapper.Map<BookFilterDto>(rq);
        var query = new BookFilterQuery(bookFilterDto);
        var books =  await _mediator.Send(query, callContext.CancellationToken);
        var rs = _mapper.Map<IReadOnlyCollection<GetBookRs>>(books);
        return rs;
    }

    async Task IBookGrpcService.DeleteBook(DeleteBookRq rq, CallContext callContext)
    {
        var command = new DeleteBookCommand(rq.BookId);
        await _mediator.Send(command, callContext.CancellationToken);
    }
}
