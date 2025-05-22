using Catalog.Domain.IServices;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

internal class GetBookImgeQueryHandler : IQueryHandler<GetBookImgeQuery, string>
{
    private readonly IFileService _fileSerivce;

    public GetBookImgeQueryHandler(IFileService fileSerivce)
    {
        _fileSerivce = fileSerivce;
    }

    public async Task<string> Handle(GetBookImgeQuery rq, CancellationToken ct)
    {
        var url = await _fileSerivce.GetFileAsync(rq.fileName, ct);
        return url;
    }
}
