using Microsoft.EntityFrameworkCore;

namespace ManageContacts.Entity.Contexts;

public static class ContactsContextExtensions
{
    public static bool HasData<TEntity>(this DbContext dbContext) where TEntity : class
    {
        return dbContext.Set<TEntity>().Any();
    }
}