using ManageContacts.Model.Models.Contacts;
using ManageContacts.Model.Models.PhoneTypes;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.PhoneNumbers;

public class PhoneNumberModel
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "phone")]
    public string Phone { get; set; }

    [JsonProperty(PropertyName = "type")]
    public string? Type { get; set; }
    
    [JsonProperty(PropertyName = "formatted_type")]
    public string? FormattedType { get; set; }
    
    [JsonProperty(PropertyName = "phone_type_id")]
    public Guid PhoneTypeId { get; set; }
}