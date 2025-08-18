using Catalog.Domain.IRepositories;
using MassTransit.MongoDbIntegration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly MongoDbContext _context;


        public UnitOfWork(MongoDbContext context)
        {
            _context = context;
        }

        public IClientSessionHandle Session => _context.Session!;

        public Task AbortTransaction(CancellationToken cancellationToken) => _context.AbortTransaction(cancellationToken);


        public Task BeginTransaction(CancellationToken cancellationToken) => _context.BeginTransaction(cancellationToken);

        public Task CommitTransaction(CancellationToken cancellationToken) => _context.CommitTransaction(cancellationToken);

        public void Dispose()
        {
            _context.Dispose();
        }

        public MongoDbCollectionContext<T> GetCollection<T>() => _context.GetCollection<T>();


        public Task<IClientSessionHandle> StartSession(CancellationToken cancellationToken) => _context.StartSession(cancellationToken);

    }
}
