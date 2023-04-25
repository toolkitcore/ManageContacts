using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class Contact : IFullAuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ContactId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }
 
    [Required]
    [StringLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string NickName { get; set; }
    
    public DateTime? Birthday { get; set; }
    
    [StringLength(1000)]
    public string? Note { get; set; }
    
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }

    #region [AUDIT PROPERTIES]
    public bool Deleted { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? ModifiedTime { get; set; }
    
    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    public virtual User User { get; set; }
    
    public virtual Group Group { get; set; }
    
    public virtual Company Company { get; set; }
    
    public ICollection<ContactPhone> Phones { get; set; }
    
    public ICollection<ContactEmail> Emails { get; set; }
    
    public ICollection<ContactAddress> Addresses { get; set; }
    
    public ICollection<ContactRelative> Relatives { get; set; }

    #endregion [REFERENCE PROPERTIES]
}