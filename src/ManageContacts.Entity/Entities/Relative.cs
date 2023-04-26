using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageContacts.Entity.Entities;

public class Relative
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ContactRelativeId { get; set; }
    
    [Required]
    [Phone]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(255)]
    public string? Type { get; set; }
    
    [StringLength(255)]
    public string? FormattedType { get; set; }
    
    public Guid RelativeTypeId { get; set; }
    
    public Guid ContactId { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual RelativeType RelativeType { get; set; }
    
    public virtual Contact Contact { get; set; }
    #endregion [REFERENCE PROPERTIES]
}