using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public sealed class GetPublishersQueryHandler : IQueryHandler<GetPublishersQuery, IReadOnlyCollection<PublisherDto>>
{
    private readonly IPubliserRepository _repository;

    public GetPublishersQueryHandler(IPubliserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<PublisherDto>> Handle(GetPublishersQuery request, CancellationToken ct)
    {
        var publishers = await _repository.GetAll(ct);
        return publishers;
    }
}
