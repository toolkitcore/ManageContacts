using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class UserRole : AuditEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual User Creator { get; set; }
    
    public virtual User User { get; set; }
    
    public virtual Role Role { get; set; }
    #endregion
    
}