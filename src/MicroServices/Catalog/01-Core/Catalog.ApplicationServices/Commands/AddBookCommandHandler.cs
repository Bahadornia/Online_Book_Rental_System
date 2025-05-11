using Catalog.Domain.Dtos;
using Catalog.Domain.Repositories;
using Framework.CQRS;
using MapsterMapper;
using MediatR;

namespace Catalog.ApplicationServices.Commands;

public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
{
    private readonly IBookRepository _bookService;
    private readonly IMapper _mapper;

    public AddBookCommandHandler(IBookRepository bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddBookCommand command, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<BookDto>(command);
        dto.Id = Guid.NewGuid();
        dto.Image = "test";
        await _bookService.AddBook(dto, cancellationToken);
        return Unit.Value;
    }
}
