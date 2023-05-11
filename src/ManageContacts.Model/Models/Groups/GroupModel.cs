using ManageContacts.Model.Models.Contacts;
using ManageContacts.Model.Models.Users;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Groups;

public class GroupModel
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    
    [JsonProperty(PropertyName = "description")]
    public string? Description { get; set; }
    
    [JsonProperty("deleted")]
    public bool Deleted { get; set; }
    
    [JsonProperty("created_time")]
    public DateTime CreatedTime { get; set; }
    
    [JsonProperty("user_id")]
    public Guid? UserId { get; set; }
    
    [JsonProperty("user")]
    public UserModel User { get; set; }
    
    [JsonProperty(PropertyName = "contacts")]
    public IEnumerable<ContactModel> Contacts { get; set; }
}