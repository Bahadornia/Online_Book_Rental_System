using Framework.CQRS;

namespace Catalog.ApplicationServices.Queries
{
    public record GetBookImgeQuery(string fileName): IQuery<string>;
}
