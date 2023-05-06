using EFCore.BulkExtensions;
using ManageContacts.Entity.Abstractions.Audits.Interfaces;
using ManageContacts.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ManageContacts.Entity.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public ApplicationDbContext() { }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        this.ApplyAuditFieldsToModifiedEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        this.ApplyAuditFieldsToModifiedEntities();
        return base.SaveChanges();
    }
    
    #region [Private methods]
    private void ApplyAuditFieldsToModifiedEntities()
    {
        var modified = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added 
                        || e.State == EntityState.Modified
                        || e.State == EntityState.Deleted);

        foreach (var entry in modified)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity is ICreationAuditEntity creationAuditEntity)
                    {
                        creationAuditEntity.CreatedTime = DateTime.UtcNow;
                        entry.State = EntityState.Added;
                    }
                    if (entry.Entity is IDeletionAuditEntity deletion)
                    {
                        deletion.Deleted = false;
                    }
                    break;
                case EntityState.Modified:
                    Entry(entry.Entity).Property("Id").IsModified = false;
                    if (entry.Entity is IModificationAuditEntity modificationAuditEntity)
                    {
                        modificationAuditEntity.ModifiedTime = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                    }
                    break;
                case EntityState.Deleted:
                    Entry(entry.Entity).Property("Id").IsModified = false;
                    if (entry.Entity is IDeletionAuditEntity deletionAuditEntity)
                    {
                        deletionAuditEntity.Deleted = true;
                        deletionAuditEntity.DeletedTime = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                    }
                    break;
            }
        }
    }
    #endregion
    
}