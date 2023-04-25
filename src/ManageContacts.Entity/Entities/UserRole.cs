using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class UserRole : ICreationAuditEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    
    #region [AUDIT PROPERTIES]
    public DateTime CreatedTime { get; set; }
    
    public Guid? CreatorId { get; set; }
    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    public virtual User Creator { get; set; }
    
    public virtual User User { get; set; }
    
    public virtual Role Role { get; set; }
    #endregion

}