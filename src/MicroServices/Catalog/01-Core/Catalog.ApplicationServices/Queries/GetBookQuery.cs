using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;
using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries;

public record GetBookQuery(long Id): IQuery<BookDto>;
