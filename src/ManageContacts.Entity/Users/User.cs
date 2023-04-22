using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Users;

public class User : IFullAuditEntity
{
    public Guid UserId { get; set; }
    
    public string UserName { get; set; }
    
    public string PasswordHashed { get; set; }
    
    public string PasswordSalt { get; set; }
    
    public string Email { get; set; }
    
    public bool EmailConfirmed { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Avatar { get; set; }
    
    #region [AUDIT PROPERTIES]
    public bool Deleted { get; set; }
    public DateTime CreatedTime { get; set; }
    public Guid? CreatorId { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public Guid? ModifierId { get; set; }
    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    [ForeignKey(nameof(CreatorId))]
    public virtual User Creator { get; set; }

    [ForeignKey(nameof(ModifierId))]
    public virtual User Modifier { get; set; }
    #endregion
    
}