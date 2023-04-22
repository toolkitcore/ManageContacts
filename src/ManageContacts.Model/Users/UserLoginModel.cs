using Newtonsoft.Json;

namespace ManageContacts.Model.Users;

public class UserLoginModel
{
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("password")]
    public string Password { get; set; }
}