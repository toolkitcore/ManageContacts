using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class EmailType : IFullAuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid EmailTypeId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string TypeName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string UnaccentedName { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }

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
    #endregion [REFERENCE PROPERTIES]
}