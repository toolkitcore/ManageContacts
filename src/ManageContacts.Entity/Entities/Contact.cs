using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class Contact : AuditEntity
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
    public Guid? GroupId { get; set; }

    #region [REFERENCE PROPERTIES]
    public virtual User User { get; set; }

    public virtual Group Group { get; set; }
    
    public virtual Company Company { get; set; }
    
    public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    
    
    #endregion [REFERENCE PROPERTIES]
}