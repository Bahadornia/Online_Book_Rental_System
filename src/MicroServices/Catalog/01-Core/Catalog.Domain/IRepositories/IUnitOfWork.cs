namespace Catalog.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        Task BeginTransaction(CancellationToken cancellationToken);
        Task CommitTransaction(CancellationToken cancellationToken);
        Task AbortTransaction(CancellationToken cancellationToken);
    }
}
