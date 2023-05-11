using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Roles;

public class RoleModel
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    
    [JsonProperty(PropertyName = "description")]
    public string? Description { get; set; }
    
    [JsonProperty(PropertyName = "deleted")]
    public bool Deleted { get; set; }
}