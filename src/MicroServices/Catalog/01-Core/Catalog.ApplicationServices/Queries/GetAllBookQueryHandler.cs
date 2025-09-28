using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public class GetAllBookQueryHandler : IQueryHandler<GetAllBookQuery, AllBooksDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IFileService _fileService;

    public GetAllBookQueryHandler(IBookRepository bookRepository, IFileService fileService)
    {
        _bookRepository = bookRepository;
        _fileService = fileService;
    }

    public async Task<AllBooksDto> Handle(GetAllBookQuery request, CancellationToken ct)
    {
        var agGridRequest = request.AgGridRequest;
        var rs = await _bookRepository.GetAll(agGridRequest, ct);

        var tasks = rs.Books.Select(async (item) =>
        {
            var url = await _fileService.GetFileAsync($"thumbnails/{item.ImageUrl}", ct);
            item.ImageUrl = url;
        });

        await Task.WhenAll(tasks);
        return rs;
    }
}
