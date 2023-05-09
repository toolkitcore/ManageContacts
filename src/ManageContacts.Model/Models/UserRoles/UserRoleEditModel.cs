namespace ManageContacts.Model.Models.UserRoles;

public class UserRoleEditModel
{
    public Guid UserId { get; set; }
    public List<Guid>? ListRoleId { get; set; }
}