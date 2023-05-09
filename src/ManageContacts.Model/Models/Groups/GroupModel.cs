using ManageContacts.Model.Models.Contacts;
using ManageContacts.Model.Models.Users;

namespace ManageContacts.Model.Models.Groups;

public class GroupModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Deleted { get; set; }
    public DateTime CreatedTime { get; set; }
    public Guid? UserId { get; set; }
    public virtual UserModel User { get; set; }
    public IEnumerable<ContactModel> Contacts { get; set; }
}