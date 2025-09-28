using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Framework.CQRS;
using MapsterMapper;

namespace Catalog.ApplicationServices.Queries
{
    internal class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IReadOnlyCollection<CategoryDto>>
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll(request.Term, cancellationToken);
            return _mapper.Map<IReadOnlyCollection<CategoryDto>>(categories);
        }
    }
}
