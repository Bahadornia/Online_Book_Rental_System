using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public class GetBooksByIdsQueryHandler : IQueryHandler<GetBooksByIdsQuery, IReadOnlyCollection<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksByIdsQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IReadOnlyCollection<BookDto>> Handle(GetBooksByIdsQuery request, CancellationToken ct)
    {
        var books = await _bookRepository.GetBooksByIds(request.Ids, ct);
        return books;
    }
}
