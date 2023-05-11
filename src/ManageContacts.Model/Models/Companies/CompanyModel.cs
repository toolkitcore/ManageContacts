using ManageContacts.Model.Models.Contacts;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Companies;

public class CompanyModel
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    
    [JsonProperty(PropertyName = "description")]
    public string? Description { get; set; }
}