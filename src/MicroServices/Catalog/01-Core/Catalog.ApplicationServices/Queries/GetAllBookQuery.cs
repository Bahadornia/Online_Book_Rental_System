using Catalog.Domain.Dtos;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public record GetAllBookQuery : IQuery<IReadOnlyCollection<BookDto>>;

