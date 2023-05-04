using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class PhoneType : FullAuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PhoneTypeId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string TypeName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string UnaccentedName { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual User Creator { get; set; }
    
    public virtual User Modifier { get; set; }
    
    #endregion [REFERENCE PROPERTIES]
}