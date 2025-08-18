using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.BookAggregate;
using MapsterMapper;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    internal class PublisherRepository : IPubliserRepository
    {
        private readonly CatalogDbContext _dbContext;
        private readonly IMapper _mapper;

        public PublisherRepository(CatalogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Add(PublisherDto publisher, CancellationToken ct)
        {
            var publisherData = new PublisherData { Name = publisher.Name };
            await _dbContext.Publishers.InsertOneAsync(publisherData, null, ct);
        }

        async Task<IReadOnlyCollection<PublisherDto>> IPubliserRepository.GetAll(CancellationToken ct)
        {
            var filter = Builders<PublisherData>.Filter.Empty;
            var publishersData = await _dbContext.Publishers.Find(filter).ToListAsync(ct);
            return _mapper.Map<IReadOnlyCollection<PublisherDto>>(publishersData);
        }
    }
}
