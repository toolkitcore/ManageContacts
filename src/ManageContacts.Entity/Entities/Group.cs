using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class Group : AuditEntity
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    
    public string? Description { get; set; }

    #region [REFERENCE PROPERTIES]
    public virtual User User { get; set; }
    public ICollection<Contact> Contacts { get; set; }
    #endregion [REFERENCE PROPERTIES]
}