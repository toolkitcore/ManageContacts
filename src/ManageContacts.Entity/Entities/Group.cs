using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class Group 
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    
    public string? Description { get; set; }

    #region [AUDIT PROPERTIES]
    public bool Deleted { get; set; }
    public DateTime CreatedTime { get; set; }
    
    public Guid? UserId { get; set; }

    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    public virtual User User { get; set; }
    public ICollection<Contact> Contacts { get; set; }
    #endregion [REFERENCE PROPERTIES]
}