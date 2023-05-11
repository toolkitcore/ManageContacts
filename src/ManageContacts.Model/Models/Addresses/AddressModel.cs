using ManageContacts.Model.Models.AddressTypes;
using ManageContacts.Model.Models.Contacts;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Addresses;

public class AddressModel
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "province")]
    public string Province { get; set; }
        
    [JsonProperty(PropertyName = "ward")]
    public string Ward { get; set; }
        
    [JsonProperty(PropertyName = "district")]
    public string District { get; set; }
        
    [JsonProperty(PropertyName = "address")]
    public string Addresss { get; set; }
        
    [JsonProperty(PropertyName = "type")]
    public string? Type { get; set; }
        
    [JsonProperty(PropertyName = "formatted_type")]
    public string? FormattedType { get; set; }
        
    [JsonProperty(PropertyName = "address_type_id")]
    public Guid AddressTypeId { get; set; }
}