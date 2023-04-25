using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;
using ManageContacts.Shared.AttributeExtensions;

namespace ManageContacts.Entity.Entities;

public class User : IFullAuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserId { get; set; }
    
    [Required]
    [EmailOrPhone]
    [StringLength(255)]
    public string UserName { get; set; }
    
    [Required]
    public string PasswordHashed { get; set; }
    
    [Required]
    [StringLength(32)]
    public string PasswordSalt { get; set; }
    
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [Phone]
    [StringLength(255)]
    public string PhoneNumber { get; set; }
    
    public string? Avatar { get; set; }
    
    #region [AUDIT PROPERTIES]
    public bool Deleted { get; set; }
    public DateTime CreatedTime { get; set; }
    
    public Guid? CreatorId { get; set; }
    public DateTime? ModifiedTime { get; set; }
    
    public Guid? ModifierId { get; set; }
    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    public virtual User Creator { get; set; }
    
    public virtual User Modifier { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
    
    public ICollection<Contact> Contacts { get; set; }
    
    #endregion [REFERENCE PROPERTIES]
}