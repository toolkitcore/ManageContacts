using Newtonsoft.Json;

namespace ManageContacts.Model.Models.AddressTypes;

public class AddressTypeModel
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "type_name")]
    public string TypeName { get; set; }
    
    [JsonProperty(PropertyName = "unaccented_name")]
    public string UnaccentedName { get; set; }
    
    [JsonProperty(PropertyName = "description")]
    public string? Description { get; set; }
}