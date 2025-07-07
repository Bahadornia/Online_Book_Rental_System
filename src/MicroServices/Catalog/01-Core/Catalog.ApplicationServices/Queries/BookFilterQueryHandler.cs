using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public class BookFilterQueryHandler : IQueryHandler<BookFilterQuery, IReadOnlyCollection<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IFileService _fileService;

    public BookFilterQueryHandler(IBookRepository bookRepository, IFileService fileService)
    {
        _bookRepository = bookRepository;
        _fileService = fileService;
    }

    public async Task<IReadOnlyCollection<BookDto>> Handle(BookFilterQuery request, CancellationToken ct)
    {
        var bookFilterDto = request.BookDto;
        var rs = await _bookRepository.SearchBook(bookFilterDto, ct);
        var tasks = rs.Select(async (item) =>
        {
            var url = await _fileService.GetFileAsync($"thumbnails/{item.ImageUrl}", ct);
            item.ImageUrl = url;
        });
        await Task.WhenAll(tasks);
        return rs;
    }
}
