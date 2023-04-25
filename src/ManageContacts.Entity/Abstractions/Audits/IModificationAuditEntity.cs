namespace ManageContacts.Entity.Abstractions.Audits;

public interface IModificationAuditEntity
{
    DateTime? ModifiedTime { get; set; }
    
}