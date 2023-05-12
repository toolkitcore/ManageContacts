using Newtonsoft.Json;

namespace ManageContacts.Model.Models.RelativeTypes;

public class RelativeTypeModel
{
    public Guid Id { get; set; }
    public string TypeName { get; set; }
    public string UnaccentedName { get; set; }
    public string? Description { get; set; }
}