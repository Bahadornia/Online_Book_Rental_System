using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public class GetBookQueryHandler : IQueryHandler<GetBookQuery, BookDto>
{
    private readonly IBookRepository _bookRepository;

    public GetBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookById(request.Id, cancellationToken);
        return book;
    }
}
