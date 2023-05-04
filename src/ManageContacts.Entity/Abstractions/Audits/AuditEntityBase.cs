using ManageContacts.Entity.Abstractions.Audits.Interfaces;

namespace ManageContacts.Entity.Abstractions.Audits;

public class AuditEntityBase : ICreationAuditEntity, IModificationAuditEntity
{
    public DateTime CreatedTime { get; set; }
    public Guid? CreatorId { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public Guid? ModifierId { get; set; }
}