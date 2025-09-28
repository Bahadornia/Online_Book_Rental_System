using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Minio.DataModel.Args;
using StackExchange.Redis;
using System.Text.Json;

namespace Catalog.Infrastructure.Repositories;

internal class CachedCategoryRepository : ICategoryRepository
{
    private const string HASH_KEY = "CATALOG_APP";
    private const string KEY = "CATEGORIES";
    private readonly ICategoryRepository _repository;
    private readonly IDatabase _db;

    public CachedCategoryRepository(ICategoryRepository repository, IConnectionMultiplexer redis)
    {
        _repository = repository;
        _db = redis.GetDatabase(2);
    }

    public void Add(CategoryDto publisher)
    {
        _repository.Add(publisher);
    }

    public async Task<IReadOnlyCollection<Category>> GetAll(string term, CancellationToken ct)
    {
        var cachedResult = await _db.HashGetAsync(HASH_KEY, KEY);
        if (!cachedResult.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<IReadOnlyCollection<Category>>(cachedResult)!;
        }

        var categories = await _repository.GetAll(term, ct);
        if (categories.Count > 0)
        {
        var cacheInput = JsonSerializer.Serialize(categories);
        await _db.HashSetAsync(HASH_KEY, KEY, cacheInput);
        await _db.KeyExpireAsync(HASH_KEY, TimeSpan.FromHours(1));
        }
        return categories;
    }
}
