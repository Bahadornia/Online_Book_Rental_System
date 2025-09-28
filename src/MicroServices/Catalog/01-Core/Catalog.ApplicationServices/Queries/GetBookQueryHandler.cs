using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Framework.CQRS;
using MapsterMapper;

namespace Catalog.ApplicationServices.Queries;

public class GetBookQueryHandler : IQueryHandler<GetBookQuery, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetById(request.Id, cancellationToken);
        return _mapper.Map<BookDto>(book);
    }
}
