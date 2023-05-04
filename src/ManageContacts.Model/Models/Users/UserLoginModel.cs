using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Users;

public class UserLoginModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}