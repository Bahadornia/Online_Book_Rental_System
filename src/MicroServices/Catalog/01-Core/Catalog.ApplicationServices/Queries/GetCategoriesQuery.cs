using Catalog.Domain.Dtos;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public record class GetCategoriesQuery(string Term) :IQuery<IReadOnlyCollection<CategoryDto>>;
