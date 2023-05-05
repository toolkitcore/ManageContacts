using System.Data;
using ManageContacts.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ManageContacts.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    bool SaveChanges();

    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

    void Rollback();

    void Commit();

    Task CommitAsync(CancellationToken cancellationToken = default);

    void BeginTransaction();

    void SetIsolationLevel(IsolationLevel isolationLevel);
        
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    
}