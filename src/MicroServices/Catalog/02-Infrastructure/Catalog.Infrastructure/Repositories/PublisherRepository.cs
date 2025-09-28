using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    internal class PublisherRepository : IPubliserRepository
    {
        private readonly CatalogDbContext _dbContext;

        public PublisherRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(PublisherDto dto)
        {
            var publisher = new Publisher
            {
                Name = dto.Name,
            };
            _dbContext.Publishers.Add(publisher);
        }

        async Task<IReadOnlyCollection<Publisher>> IPubliserRepository.GetAll(string term, CancellationToken ct)
        {
            var publishers = await _dbContext.Publishers.Where(p => p.Name.Contains(term)).AsNoTracking().ToListAsync(ct);
            return publishers.AsReadOnly();
        }
    }
}
