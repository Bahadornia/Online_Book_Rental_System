using MongoDB.Driver;

namespace Catalog.Domain.IRepositories
{
    public interface IUnitOfWork
    {
        Task BeginTransaction(CancellationToken cancellationToken);
        Task CommitTransaction(CancellationToken cancellationToken);
        Task AbortTransaction(CancellationToken cancellationToken);
        public IClientSessionHandle Session { get;}
    }
}
