using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Roles;

public class Role : IFullAuditEntity
{
    public Guid RoleId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    #region [AUDIT PROPERTIES]
    public bool Deleted { get; set; }

    public DateTime CreatedTime { get; set; }

    public Guid? CreatorId { get; set; }

    public DateTime? ModifiedTime { get; set; }

    public Guid? ModifierId { get; set; }
    #endregion [AUDIT PROPERTIES]
}