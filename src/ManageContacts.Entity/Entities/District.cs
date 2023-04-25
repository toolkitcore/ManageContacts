using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageContacts.Entity.Entities;

public class District
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DistrictId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    public string Status { get; set; }
    
    public Guid ProvinceId { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual Province Province { get; set; }
    #endregion [REFERENCE PROPERTIES]
}