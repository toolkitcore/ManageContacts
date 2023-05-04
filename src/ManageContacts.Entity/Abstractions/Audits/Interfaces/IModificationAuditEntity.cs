namespace ManageContacts.Entity.Abstractions.Audits.Interfaces;

public interface IModificationAuditEntity
{
    DateTime? ModifiedTime { get; set; }
    Guid? ModifierId { get; set; }
}