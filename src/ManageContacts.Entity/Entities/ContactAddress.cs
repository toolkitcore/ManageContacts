using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageContacts.Entity.Entities;

public class ContactAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ContactAddressId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Province { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Ward { get; set; }
    
    [Required]
    [StringLength(100)]
    public string District { get; set; }
    
    [StringLength(1000)]
    public string Address { get; set; }
    
    public Guid ContactId { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual Contact Contact { get; set; }
    #endregion [REFERENCE PROPERTIES]
}