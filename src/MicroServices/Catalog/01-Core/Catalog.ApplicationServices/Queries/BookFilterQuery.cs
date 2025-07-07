using Catalog.Domain.Dtos;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public record BookFilterQuery(BookFilterDto BookDto): IQuery<IReadOnlyCollection<BookDto>>;

