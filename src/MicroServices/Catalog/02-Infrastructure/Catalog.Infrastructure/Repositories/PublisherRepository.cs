using Catalog.Domain.IRepositories;
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

        public async Task AddPubliser(string publisher, CancellationToken ct)
        {
            await _dbContext.Publishers.InsertOneAsync(publisher, null, ct);
        }

        public Task<IReadOnlyCollection<string>> GetAll(string publisher, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
