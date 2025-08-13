using Catalog.Domain.IRepositories;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories
{
    internal class PublisherRepository : IPubliserRepository
    {
        private readonly CatalogDbContext _dbContext;

        public PublisherRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Publisher publisher, CancellationToken ct)
        {
            await _dbContext.Publishers.InsertOneAsync(publisher, null, ct);
        }

        public Task<IReadOnlyCollection<string>> GetAll(string publisher, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyCollection<Publisher>> IPubliserRepository.GetAll(string publisher, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
