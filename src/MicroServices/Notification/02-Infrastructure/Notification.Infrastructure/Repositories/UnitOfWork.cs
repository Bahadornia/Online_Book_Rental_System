using MassTransit.MongoDbIntegration;
using Notification.Domain.IRepositories;

namespace Notification.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitofWork, IDisposable
{
    private readonly MongoDbContext _context;

    public UnitOfWork(MongoDbContext context)
    {
        _context = context;
    }

    public Task AbortTransaction(CancellationToken ct) => _context.AbortTransaction(ct);


    public Task BeginTransaction(CancellationToken ct) => _context.BeginTransaction(ct);
   
    public Task CommitTransaction(CancellationToken ct)=> _context.CommitTransaction(ct);
   

    public void Dispose()
    {
       _context.Dispose();
    }
}
