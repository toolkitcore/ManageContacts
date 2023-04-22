using System.Linq.Expressions;
using ManageContacts.Entity.Abstractions.Audits;
using ManageContacts.Entity.Abstractions.Paginations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManageContacts.Infrastructure.Abstractions;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
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
        Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
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
    

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true,
        CancellationToken cancellationToken = default)
        => await Query(predicate, orderBy, include, disableTracking).FirstOrDefaultAsync(cancellationToken);

    public void Insert(TEntity entity) => _dbSet.Add(entity);
    public void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);
    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default) => await _dbSet.AddAsync(entity, cancellationToken);
    public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => await _dbSet.AddRangeAsync(entities, cancellationToken);
    public void Update(TEntity entity) => _dbSet.Update(entity);
    public void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);
    public void Delete(TEntity entity) => _dbSet.Remove(entity);
    public void Delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    public void Remove(TEntity entity)
    {
        if (entity is IDeletionAuditEntity deletionAuditEntity)
            deletionAuditEntity.Deleted = true;

        _dbSet.Update(entity);
    }

    public void Remove(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is IDeletionAuditEntity deletionAuditEntity)
                deletionAuditEntity.Deleted = true;
        }

        _dbSet.UpdateRange(entities);
    }

    public bool SaveChanges() => _dbContext.SaveChanges() > 0;

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) 
        => await _dbContext.SaveChangesAsync(cancellationToken) > 0;

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        => await _dbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task EndTransactionAsync(CancellationToken cancellationToken)
    {
         await _dbContext.SaveChangesAsync(cancellationToken);
         await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        => await _dbContext.Database.RollbackTransactionAsync(cancellationToken);

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