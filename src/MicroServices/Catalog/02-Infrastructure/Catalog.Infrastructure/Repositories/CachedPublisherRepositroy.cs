using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Catalog.Infrastructure.Repositories;

internal class CachedPublisherRepositroy : IPubliserRepository
{
    private const string HASH_KEY = "CATALOG_APP";
    private const string KEY = "PUBLISHERS";
    private readonly IPubliserRepository _repository;
    private readonly IDatabase _db;

    public CachedPublisherRepositroy(IPubliserRepository repository, IConnectionMultiplexer redis)
    {
        _repository = repository;
        _db = redis.GetDatabase(2);
    }

    public async Task Add(PublisherDto publisher, CancellationToken ct)
    {
        await _repository.Add(publisher, ct);
    }

    public async Task<IReadOnlyCollection<PublisherDto>> GetAll(CancellationToken ct)
    {
        var cacheResult = await _db.HashGetAsync(HASH_KEY, KEY);
        if (!cacheResult.IsNullOrEmpty)
        {
            var rs = JsonSerializer.Deserialize<IReadOnlyCollection<PublisherDto>>(cacheResult);
            return rs;
        }
        var publishers = await _repository.GetAll(ct);
        var redisValue = JsonSerializer.Serialize(publishers);
        await _db.HashSetAsync(HASH_KEY, KEY, redisValue);
        return publishers;
    }
}
