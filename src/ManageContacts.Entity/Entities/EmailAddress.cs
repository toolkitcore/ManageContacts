using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageContacts.Entity.Entities;

public class EmailAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }
    
    [StringLength(255)]
    public string? Type { get; set; }
    
    [StringLength(255)]
    public string? FormattedType { get; set; }
    
    public Guid EmailTypeId { get; set; }
    
    public Guid ContactId { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual EmailType EmailType { get; set; }
    
    public virtual Contact Contact { get; set; }
    #endregion [REFERENCE PROPERTIES]
}