using Catalog.API.Grpc.Client.Logics;
using Catalog.API.Grpc.Client.Responses;
using Catalog.ApplicationServices.Queries;
using MapsterMapper;
using MediatR;
using ProtoBuf.Grpc;

namespace Catalog.API.Grpc.Services;

internal class CategoryService : ICategoryService
{
    private readonly IMediator _mediator;
    private IMapper _mapper;

    public CategoryService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetCategoryRs>> GetAll(string term, CallContext callContext)
    {
        var query = new GetCategoriesQuery(term);
        var rs = await _mediator.Send(query, callContext.CancellationToken);
        var categories = _mapper.Map<IReadOnlyCollection<GetCategoryRs>>(rs);
        return categories;
    }
}
