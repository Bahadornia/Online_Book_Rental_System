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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublisherRepository(CatalogDbContext dbContext, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(PublisherDto dto, CancellationToken ct)
        {
            var publisher = Publisher.Create(dto.Name);
            _dbContext.Publishers.Add(publisher);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        async Task<IReadOnlyCollection<PublisherDto>> IPubliserRepository.GetAll(CancellationToken ct)
        {
            var publishers = _dbContext.Publishers.AsNoTracking().ToListAsync(ct);
            return _mapper.Map<IReadOnlyCollection<PublisherDto>>(publishers);
        }
    }
}
