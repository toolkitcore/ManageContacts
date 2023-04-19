using Microsoft.EntityFrameworkCore.Storage;

namespace ManageContacts.Infrastructure.Abstractions;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    void Insert(TEntity entity);
        
    void Insert(IEnumerable<TEntity> entities);
        
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        
    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        
    void Update(TEntity entity);
        
    void Update(IEnumerable<TEntity> entities);
        
    void Delete(TEntity entity);
        
    void Delete(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void Remove(IEnumerable<TEntity> entities);

    bool SaveChanges();
    
    bool SaveChangesAsync();

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task EndTransactionAsync();
    
    Task RollbackTransactionAsync();
}