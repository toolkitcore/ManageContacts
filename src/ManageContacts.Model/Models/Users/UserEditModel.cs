using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Users;

public class UserEditModel
{
    [JsonProperty("user_name")]
    public string UserName { get; set; }
    
    [JsonProperty("password")]
    public string Password { get; set; }
    
    [JsonProperty("first_name")]
    public string FirstName { get; set; }
    
    [JsonProperty("last_name")]
    public string LastName { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; }
    
    [JsonProperty("avatar")]
    public string? Avatar { get; set; }

}