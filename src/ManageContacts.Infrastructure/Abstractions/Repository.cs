using System.Linq.Expressions;
using ManageContacts.Entity.Abstractions.Paginations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManageContacts.Infrastructure.Abstractions;

public class Repository <TEntity> : IRepositoryQueryBase<TEntity>, IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IPagedList<TEntity> PagingAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true,
        int pageIndex = 1, int pageSize = 25, int indexFrom = 1)
    {
        throw new NotImplementedException();
    }

    public async Task<IPagedList<TEntity>> PagingAllAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true,
        int pageIndex = 1, int pageSize = 25, int indexFrom = 1, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public TEntity Get(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Insert(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Insert(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public void Remove(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public bool SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task EndTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task RollbackTransactionAsync()
    {
        throw new NotImplementedException();
    }
}