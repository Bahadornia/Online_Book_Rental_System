using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using MapsterMapper;
using StackExchange.Redis;
using System.Text.Json;

namespace Catalog.Infrastructure.Repositories;

internal class CachedPublisherRepositroy : IPubliserRepository
{
    private const string HASH_KEY = "CATALOG_APP";
    private const string KEY = "PUBLISHERS";
    private readonly IPubliserRepository _repository;
    private readonly IDatabase _db;
    private readonly IMapper _mapper;

    public CachedPublisherRepositroy(IPubliserRepository repository, IConnectionMultiplexer redis, IMapper mapper)
    {
        _repository = repository;
        _db = redis.GetDatabase(2);
        _mapper = mapper;
    }

    public void Add(PublisherDto publisher)
    {
        _repository.Add(publisher);
    }

    public async Task<IReadOnlyCollection<Publisher>> GetAll(string term, CancellationToken ct)
    {
        var cacheResult = await _db.HashGetAsync(HASH_KEY, KEY);
        if (!cacheResult.IsNullOrEmpty)
        {
            var rs = JsonSerializer.Deserialize<IReadOnlyCollection<Publisher>>(cacheResult);
            return rs;
        }
        var publishers = await _repository.GetAll(term, ct);
        if (publishers.Count > 0)
        {
            var publisherDto = _mapper.Map<IReadOnlyCollection<PublisherDto>>(publishers);
            var redisValue = JsonSerializer.Serialize(publisherDto);
            await _db.HashSetAsync(HASH_KEY, KEY, redisValue);
            await _db.KeyExpireAsync(HASH_KEY, TimeSpan.FromHours(1));
        }
        return publishers;
    }
}
