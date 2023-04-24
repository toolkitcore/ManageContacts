using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;
using ManageContacts.Entity.Roles;
using ManageContacts.Entity.Users;

namespace ManageContacts.Entity.UserRoles;

public class UserRole : ICreationAuditEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    
    #region [AUDIT PROPERTIES]
    public DateTime CreatedTime { get; set; }
    public Guid? CreatorId { get; set; }
    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    [ForeignKey(nameof(CreatorId))]
    public virtual User Creator { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; }
    #endregion

}