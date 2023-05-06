using System.Linq.Expressions;
using EFCore.BulkExtensions;
using ManageContacts.Entity.Abstractions.Audits;
using ManageContacts.Entity.Abstractions.Audits.Interfaces;
using ManageContacts.Entity.Abstractions.Paginations;
using ManageContacts.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManageContacts.Infrastructure.Abstractions;

public class Repository<TEntity> : IRepository<TEntity> 
    where TEntity : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<TEntity>();
    }

    public IEnumerable<TEntity> FindAll(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true)
        => Query(predicate, orderBy, include, disableTracking).ToList();

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
        bool disableTracking = true,
        CancellationToken cancellationToken = default)
        => await Query(predicate, orderBy, include, disableTracking).ToListAsync(cancellationToken);

    public IPagedList<TEntity> PagingAll(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        int pageIndex = 1,
        int pageSize = 10, 
        int indexFrom = 1)
        => Query(predicate, orderBy, include, disableTracking).ToPagedList(pageIndex, pageSize, indexFrom);
        

    public async Task<IPagedList<TEntity>> PagingAllAsync(Expression<Func<TEntity, bool>> predicate = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
        bool disableTracking = true,
        int pageIndex = 1, 
        int pageSize = 10, 
        int indexFrom = 1, 
        CancellationToken cancellationToken = default)
        => await Query(predicate, orderBy, include, disableTracking).ToPagedListAsync(pageIndex, pageSize, indexFrom, cancellationToken);

    public TEntity Get(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true)
        => Query(predicate, orderBy, include, disableTracking).FirstOrDefault();
    

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default)
        => await Query(predicate, orderBy, include, disableTracking).FirstOrDefaultAsync(cancellationToken);

    public void Insert(TEntity entity) 
        => _dbSet.Add(entity);
    
    public void Insert(IList<TEntity> entities) 
        => _dbContext.AddRange(entities);

    public void BulkInsert<TEntity>(IList<TEntity> listEntities) where TEntity : class
    {
        
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default) 
        => await _dbSet.AddAsync(entity, cancellationToken);
        
    public async Task InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default) 
        => await _dbContext.BulkInsertAsync<TEntity>(entities, cancellationToken: cancellationToken);

    public async Task BulkInsertAsync<TEntity>(IList<TEntity> listEntities, CancellationToken cancellationToken = default) where TEntity : class
    {
        foreach (var entity in listEntities)
        {
            if (entity is ICreationAuditEntity creationAuditEntity)
            {
                creationAuditEntity.CreatedTime = DateTime.UtcNow;
            }
            if (entity is IDeletionAuditEntity deletionAuditEntity)
            {
                deletionAuditEntity.Deleted = false;
            }
        }
        await _dbContext.BulkInsertAsync<TEntity>(listEntities, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public void Update(TEntity entity) 
        => _dbSet.Update(entity);
    
    public void Update(IList<TEntity> entities) 
        => _dbContext.UpdateRange(entities);

    public void BulkUpdate<TEntity>(IList<TEntity> listEntities) where TEntity : class
    {
        foreach (var entity in listEntities)
        {
            if (entity is IModificationAuditEntity modificationAuditEntity)
            {
                modificationAuditEntity.ModifiedTime = DateTime.UtcNow;
            }
        }
        _dbContext.BulkUpdate<TEntity>(listEntities);
    }
    

    public async Task BulkUpdateAsync<TEntity>(IList<TEntity> listEntities, CancellationToken cancellationToken = default) where TEntity : class
    {
        foreach (var entity in listEntities)
        {
            if (entity is IModificationAuditEntity modificationAuditEntity)
            {
                modificationAuditEntity.ModifiedTime = DateTime.UtcNow;
            }
        }
        await _dbContext.BulkUpdateAsync<TEntity>(listEntities, cancellationToken: cancellationToken).ConfigureAwait(false);

    }

    public void Delete(TEntity entity) 
        => _dbSet.Remove(entity);

    public void BulkDelete<TEntity>(IList<TEntity> listEntities) where TEntity : class
    {
        if (listEntities.NotNullOrEmpty() && listEntities.FirstOrDefault() is IDeletionAuditEntity)
        {
            foreach (var entity in listEntities)
            {
                if (entity is IDeletionAuditEntity deletionAuditEntity)
                {
                    deletionAuditEntity.Deleted = true;
                    deletionAuditEntity.DeletedTime = DateTime.UtcNow;
                }
            }
            
            _dbContext.BulkUpdate<TEntity>(listEntities);
        }
        else _dbContext.BulkDelete<TEntity>(listEntities);
    }

    public async Task BulkDeleteAsync<TEntity>(IList<TEntity> listEntities, CancellationToken cancellationToken = default) where TEntity : class
    {
        if (listEntities.NotNullOrEmpty() && listEntities.FirstOrDefault() is IDeletionAuditEntity)
        {
            foreach (var entity in listEntities)
            {
                if (entity is IDeletionAuditEntity deletionAuditEntity)
                {
                    deletionAuditEntity.Deleted = true;
                    deletionAuditEntity.DeletedTime = DateTime.UtcNow;
                }
            }
            
            await _dbContext.BulkUpdateAsync<TEntity>(listEntities, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        else await _dbContext.BulkDeleteAsync<TEntity>(listEntities, cancellationToken: cancellationToken).ConfigureAwait(false);

    }

    public bool SaveChanges() 
        => _dbContext.SaveChanges() > 0;

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) 
        => await _dbContext.SaveChangesAsync(cancellationToken) > 0;


    #region [Private Methods]
    private IQueryable<TEntity> Query(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            query = orderBy(query);

        return query;
    }
    #endregion
}