namespace ManageContacts.Infrastructure.Abstractions;

public interface IRepository<TEntity> : IRepositoryQueryBase<TEntity>, IRepositoryBase<TEntity> where TEntity : class
{
    
}