using Framework.CQRS;

namespace Inventory.ApplicationServices.Queries;

public record IsBookAvailableQuery(long BookId): IQuery<bool>;

