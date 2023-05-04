namespace ManageContacts.Entity.Abstractions.Audits.Interfaces;

public interface IDeletionAuditEntity
{
    DateTime? DeletedTime { get; set; }
    bool Deleted { get; set; }
    
}