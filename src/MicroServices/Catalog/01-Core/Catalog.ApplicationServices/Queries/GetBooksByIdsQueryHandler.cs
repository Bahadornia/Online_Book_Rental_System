using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Framework.CQRS;
using MapsterMapper;

namespace Catalog.ApplicationServices.Queries;

public class GetBooksByIdsQueryHandler : IQueryHandler<GetBooksByIdsQuery, IReadOnlyCollection<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBooksByIdsQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<BookDto>> Handle(GetBooksByIdsQuery request, CancellationToken ct)
    {
        var books = await _bookRepository.GetByIds(request.Ids, ct);
        return _mapper.Map<IReadOnlyCollection<BookDto>>(books);
    }
}
