using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageContacts.Entity.Abstractions.Audits;

namespace ManageContacts.Entity.Entities;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
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
    public string Addresss { get; set; }
    
    [StringLength(255)]
    public string? Type { get; set; }
    
    [StringLength(255)]
    public string? FormattedType { get; set; }
    
    public Guid AddressTypeId { get; set; }
    
    public Guid ContactId { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual AddressType AddressType { get; set; }
    
    public virtual Contact Contact { get; set; }
    #endregion [REFERENCE PROPERTIES]
}