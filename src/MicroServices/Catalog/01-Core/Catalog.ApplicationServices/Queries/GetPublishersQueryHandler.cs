using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Framework.CQRS;
using MapsterMapper;

namespace Catalog.ApplicationServices.Queries;

public sealed class GetPublishersQueryHandler : IQueryHandler<GetPublishersQuery, IReadOnlyCollection<PublisherDto>>
{
    private readonly IPubliserRepository _repository;
    private readonly IMapper _mapper;

    public GetPublishersQueryHandler(IPubliserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<PublisherDto>> Handle(GetPublishersQuery request, CancellationToken ct)
    {
        var publishers = await _repository.GetAll(request.Term, ct);
        return _mapper.Map<IReadOnlyCollection<PublisherDto>>(publishers);
    }
}
