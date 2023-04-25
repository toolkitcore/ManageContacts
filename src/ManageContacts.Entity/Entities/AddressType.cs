using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class AddressType : IFullAuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AddressTypeId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    public Guid? ContactId { get; set; }
    
    #region [AUDIT PROPERTIES]
    public bool Deleted { get; set; }
    
    public DateTime CreatedTime { get; set; }

    public DateTime? ModifiedTime { get; set; }
    #endregion [AUDIT PROPERTIES]
    
    #region [REFERENCE PROPERTIES]
    public virtual Contact Contact { get; set; }
    #endregion [REFERENCE PROPERTIES]
}