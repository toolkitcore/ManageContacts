using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageContacts.Entity.Entities;

public class PhoneNumber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [Phone]
    [StringLength(100)]
    public string Phone { get; set; }
    
    [StringLength(255)]
    public string? Type { get; set; }
    
    [StringLength(255)]
    public string? FormattedType { get; set; }

    public Guid PhoneTypeId { get; set; }
    
    public Guid ContactId { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual PhoneType PhoneType { get; set; }
    
    public virtual Contact Contact { get; set; }
    #endregion [REFERENCE PROPERTIES]
}