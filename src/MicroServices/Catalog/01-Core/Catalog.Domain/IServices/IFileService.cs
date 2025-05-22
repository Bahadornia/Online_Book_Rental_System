namespace Catalog.Domain.IServices
{
    public interface IFileService
    {
        Task UploadFileAsync(byte[] file, string fileName, string contentType, CancellationToken ct);
        Task<string> GetFileAsync(string fileName, CancellationToken ct);
    }
}
