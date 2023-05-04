using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageContacts.Entity.Entities;

public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    public Guid ContactId { get; set; }

    #region [REFERENCE PROPERTIES]
    public virtual Contact Contact { get; set; }
    #endregion [REFERENCE PROPERTIES]
}