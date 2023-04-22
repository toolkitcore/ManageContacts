using Newtonsoft.Json;

namespace ManageContacts.Model.Users;

public class UserEditModel
{
    [JsonProperty("first_name")]
    public string FirstName { get; set; }
    
    [JsonProperty("last_name")]
    public string LastName { get; set; }
    
    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; }
    
    [JsonProperty("avatar")]
    public string Avatar { get; set; }
}