namespace ManageContacts.Entity.Abstractions.Audits.Interfaces;

public interface ICreationAuditEntity
{
    DateTime CreatedTime { get; set; }
    Guid? CreatorId { get; set; }
}