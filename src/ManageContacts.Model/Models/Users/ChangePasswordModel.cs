using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Users;

public class ChangePasswordModel
{
    public string OldPassword { get; set; }
    
    public string NewPassword { get; set; }
}