using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Framework.CQRS;
using MapsterMapper;
using MediatR;

namespace Catalog.ApplicationServices.Commands;

public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
{
    private readonly IBookRepository _bookService;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AddBookCommandHandler(IBookRepository bookService, IMapper mapper, IFileService fileService = null)
    {
        _bookService = bookService;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(AddBookCommand command, CancellationToken ct)
    {
        var dto = _mapper.Map<BookDto>(command);
        dto.Id = Guid.NewGuid();
        var fileName = await UplodaImage(dto.Id, command.Title, command.Image, command.ContentType, ct);
        dto.ImageUrl = fileName;
        await _bookService.AddBook(dto, ct);
        return Unit.Value;
    }

    private async Task<string> UplodaImage(Guid id, string title, byte[] image, string contentType, CancellationToken ct)
    {
        var extentsion = contentType.Split('/')[1];
        var fileUrl = $"{id}/{title}.{extentsion}";
        await _fileService.UploadFileAsync(image, fileUrl, contentType, ct);
        return fileUrl;
    }
}
