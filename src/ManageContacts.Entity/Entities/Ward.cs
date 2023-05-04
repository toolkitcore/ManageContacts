using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageContacts.Entity.Entities;

public class Ward
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    public bool Status { get; set; }
    
    public Guid DistrictId { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual District District { get; set; }
    #endregion [REFERENCE PROPERTIES]
}