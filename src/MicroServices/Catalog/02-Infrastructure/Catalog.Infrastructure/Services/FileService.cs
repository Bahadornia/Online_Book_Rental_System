using Catalog.Domain.IServices;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Net.Mime;

namespace Catalog.Infrastructure.Services;

public class FileService : IFileService
{
    private const int imageWidth = 50;
    private const int imageHeight = 50;
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;

    public FileService(IMinioClient minioClient, IConfiguration config)
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
        using var thumbStream = new MemoryStream();
        using (Image image = Image.Load(stream)){
        image.Mutate(x => x.Resize(imageWidth, imageHeight));
        image.SaveAsJpeg(thumbStream);
        } ;


        thumbStream.Seek(0, SeekOrigin.Begin);
        stream.Seek(0, SeekOrigin.Begin);
        try
        {

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject($"thumbnails/{fileName}")
                .WithStreamData(thumbStream)
                .WithObjectSize(thumbStream.Length)
                .WithContentType(contentType), ct);

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
            var url = await _minioClient
                .PresignedGetObjectAsync(new PresignedGetObjectArgs()
                .WithExpiry(60 * 60 * 60)
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
