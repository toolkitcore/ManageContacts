using Newtonsoft.Json;

namespace ManageContacts.Model.Models.EmailAddresses;

public class EmailAddressModel
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }
    
    [JsonProperty(PropertyName = "type")]
    public string? Type { get; set; }
    
    [JsonProperty(PropertyName = "formatted_type")]
    public string? FormattedType { get; set; }
    
    [JsonProperty(PropertyName = "email_type_id")]
    public Guid EmailTypeId { get; set; }
}