using ManageContacts.Entity.Abstractions.Audits;
using Microsoft.EntityFrameworkCore;

namespace ManageContacts.Entity.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public ApplicationDbContext() { }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modified = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified
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
                    break;
                case EntityState.Modified:
                    if (entry.Entity is IModificationAuditEntity modificationAuditEntity)
                    {
                        modificationAuditEntity.ModifiedTime = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                    }
                    break;
                case EntityState.Deleted:
                    if (entry.Entity is IDeletionAuditEntity deletionAuditEntity)
                    {
                        deletionAuditEntity.Deleted = true;
                        entry.State = EntityState.Modified;
                    }
                    break;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}