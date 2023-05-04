using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class Contact 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
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
    public Guid GroupId { get; set; }

    #region [AUDIT PROPERTIES]
    public bool Deleted { get; set; }
    
    public DateTime CreatedTime { get; set; }
    
    public Guid UserId { get; set; }
    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    public virtual User User { get; set; }

    public virtual Group Group { get; set; }
    
    public virtual Company Company { get; set; }
    
    public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    
    public ICollection<EmailAddress> EmailAddresses { get; set; }
    
    public ICollection<Address> Addresses { get; set; }
    
    public ICollection<Relative> Relatives { get; set; }
    #endregion [REFERENCE PROPERTIES]
}