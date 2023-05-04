using Microsoft.EntityFrameworkCore;

namespace ManageContacts.Infrastructure.Abstractions;

public interface IRepository<TEntity, TContext> : IRepositoryQueryBase<TEntity>, IRepositoryBase<TEntity> 
    where TEntity : class
    where TContext : DbContext
{
    
}