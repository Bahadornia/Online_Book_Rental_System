using Catalog.Domain.Dtos;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public record GetBooksByIdsQuery(IEnumerable<long> Ids): IQuery<IReadOnlyCollection<BookDto>>;
