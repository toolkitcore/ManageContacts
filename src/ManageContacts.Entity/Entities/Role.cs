using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class Role : FullAuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [StringLength(255)]
    public string? Description { get; set; }

    #region [REFERENCE PROPERTIES]
    public virtual User Creator { get; set; }
    
    public virtual User Modifier { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
    
    #endregion
    
}