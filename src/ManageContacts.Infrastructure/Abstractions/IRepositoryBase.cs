using Microsoft.EntityFrameworkCore.Storage;

namespace ManageContacts.Infrastructure.Abstractions;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    void Insert(TEntity entity);
        
    void Insert(IList<TEntity> entities);
        
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        
    Task InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);
        
    void Update(TEntity entity);
        
    void Update(IList<TEntity> entities);

    Task UpdateAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    void Delete(TEntity entity);
        
    void Delete(IList<TEntity> entities);

    Task DeleteAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    bool SaveChanges();
    
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

    Task EndTransactionAsync(CancellationToken cancellationToken);
    
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}