﻿namespace Catalog.Domain.IRepositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default!);
    }
}
