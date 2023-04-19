namespace ManageContacts.Entity.Abstractions.Audits;

public interface IDeletionAuditEntity
{
    bool Deleted { get; set; }
}