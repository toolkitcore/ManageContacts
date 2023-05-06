using Microsoft.EntityFrameworkCore.Storage;

namespace ManageContacts.Infrastructure.Abstractions;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    void Insert(TEntity entity);
        
    void Insert(IList<TEntity> entities);

    void BulkInsert<TEntity>(IList<TEntity> listEntities) where TEntity : class;
        
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        
    Task InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    Task BulkInsertAsync<TEntity>(IList<TEntity> listEntities, CancellationToken cancellationToken = default)
        where TEntity : class;
        
    void Update(TEntity entity);
        
    void Update(IList<TEntity> entities);
    
    void BulkUpdate<TEntity>(IList<TEntity> listEntities) where TEntity : class;

    Task BulkUpdateAsync<TEntity>(IList<TEntity> listEntities, CancellationToken cancellationToken = default)
        where TEntity : class;

    void Delete(TEntity entity);

    void BulkDelete<TEntity>(IList<TEntity> listEntities) where TEntity : class;

    Task BulkDeleteAsync<TEntity>(IList<TEntity> listEntities, CancellationToken cancellationToken = default)
        where TEntity : class;
    
    bool SaveChanges();
    
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    
}