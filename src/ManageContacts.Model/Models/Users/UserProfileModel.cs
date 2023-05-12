using ManageContacts.Model.Models.Roles;
using Newtonsoft.Json;

namespace ManageContacts.Model.Models.Users;

public class UserProfileModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; }
    public ICollection<RoleModel> Roles { get; set; }
}