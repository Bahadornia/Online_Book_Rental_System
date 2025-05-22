using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public class GetAllBookQueryHandler : IQueryHandler<GetAllBookQuery, IReadOnlyCollection<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IFileService _fileService;

    public GetAllBookQueryHandler(IBookRepository bookRepository, IFileService fileService)
    {
        _bookRepository = bookRepository;
        _fileService = fileService;
    }

    public async Task<IReadOnlyCollection<BookDto>> Handle(GetAllBookQuery request, CancellationToken ct)
    {
        var rs = await _bookRepository.GetAll(ct);

        var tasks = rs.Select(async (item) =>
        {
            var url = await _fileService.GetFileAsync(item.ImageUrl, ct);
            item.ImageUrl = url;
        });

        await Task.WhenAll(tasks);
        return rs;
    }
}
