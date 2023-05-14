using System.Collections.Concurrent;
using System.Data;
using ManageContacts.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ManageContacts.Infrastructure.UnitOfWork;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    protected bool _disposed = false;
    protected readonly DbContext _dbContext;
    protected IDbContextTransaction _transaction;
    protected IsolationLevel? _isolationLevel;
    protected ConcurrentDictionary<Type, object> _repositories;

    public UnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void BeginTransaction()
    {
        StartNewTransactionIfNeeded();
    }
    
    

    public void Commit()
    {
        _dbContext.SaveChanges();
        if (_transaction != null)
        {
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories == null)
            _repositories = new ConcurrentDictionary<Type, object>();

        var typeOfEntity = typeof(TEntity);
        if (!_repositories.ContainsKey(typeOfEntity))
            _repositories[typeOfEntity] = new Repository<TEntity>(_dbContext);

        return (IRepository<TEntity>)_repositories[typeOfEntity];
    }

    public void Rollback()
    {
        if (_transaction == null)
            return;

        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if(_transaction == null)
            return;
        
        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() > 0;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return (await _dbContext.SaveChangesAsync(cancellationToken)) > 0;
    }

    public void SetIsolationLevel(IsolationLevel isolationLevel)
    {
        _isolationLevel = isolationLevel;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            if (_repositories != null)
                _repositories.Clear();

            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;

            _dbContext.Dispose();
        }

        _disposed = false;
    }

    private void StartNewTransactionIfNeeded()
    {
        if (_transaction == null)
        {
            _transaction = _isolationLevel.HasValue
                ? _dbContext.Database.BeginTransaction(_isolationLevel.GetValueOrDefault())
                : _dbContext.Database.BeginTransaction();
        }
    }
}