using Catalog.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using System.IO;
using System.Net.Mime;

namespace Catalog.Infrastructure;

public class MinioService : IFileService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;

    public MinioService(IMinioClient minioClient, IConfiguration config)
    {
        _minioClient = minioClient;
        _bucketName = config["Minio:BucketName"];
    }

    public async Task UploadFileAsync(byte[] file, string fileName, string contentType, CancellationToken ct)
    {
        // Ensure the bucket exists
        bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName), ct);
        if (!found)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName), ct);
        }

        using var stream = new MemoryStream(file);
        try
        {

        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType(contentType), ct);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<string> GetFileAsync(string fileName, CancellationToken ct)
    {
        try
        {

        using var ms = new MemoryStream();
            var url = await _minioClient
                .PresignedGetObjectAsync(new PresignedGetObjectArgs()
                .WithExpiry(60 * 60*60)
                .WithBucket(_bucketName)
                .WithObject(fileName));
            return url;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
