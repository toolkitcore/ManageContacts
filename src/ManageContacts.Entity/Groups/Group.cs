using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ManageContacts.Entity.Abstractions.Audits;
using ManageContacts.Entity.Contacts;
using ManageContacts.Entity.Users;

namespace ManageContacts.Entity.Groups;

public class Group : IFullAuditEntity
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid GroupId { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
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
    public ICollection<Contact> Contacts { get; set; }
    #endregion [REFERENCE PROPERTIES]
}